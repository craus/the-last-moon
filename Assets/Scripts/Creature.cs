using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    public int gold;
    [SerializeField] private int experience;
    public int level;
    public int skillPoints;
    public int damage = 1;
    public int hp = 1;
    public int maxHp = 1;
    public bool maxedHp = true;
    public bool counterattackOn = false;
    public int slow = 0;
    public bool Alive => hp > 0;
    public bool Dead => !Alive;

    public Action<Creature, Creature, int> beforeAttack = (a, b, d) => { };
    public Action<Creature, Creature, int> afterAttack = (a, b, d) => { };

    public List<Buff> buffs = new List<Buff>();

    [SerializeField] private Transform m_buffsFolder;

    public event Action<AbilityEffect> onDeath = (s) => { };

    public Transform buffsFolder {
        get {
            if (m_buffsFolder == null) {
                var bfgo = new GameObject("Buffs");
                bfgo.transform.SetParent(transform);
                m_buffsFolder = bfgo.transform;
            }
            return m_buffsFolder;
        }
    }

    public int buffPower<T>() where T : Buff {
        return buffs.Where(b => b is T).Sum(b => b.power);
    }

    public Buff buff<T>() where T : Buff {
        return buffs.FirstOrDefault(b => b is T);
    }

    public void ApplyBuff<T>(int power = 1) where T : Buff {
        var buff = buffs.FirstOrDefault(b => b.GetType() == typeof(T));
        if (buff == null) {
            ApplyNewBuff<T>(power);
        } else {
            buff.power += power;
            buff.ExpireCheck();
        }
    }

    public Buff ApplyNewBuff<T>(int power = 1) where T : Buff {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(buffsFolder);
        var buff = go.AddComponent<T>();
        buff.power = power;
        buff.owner = this;
        return buff;
    }

    //public int armor => buffPower<Armor>();
    //public int protection => buffPower<Armor>();
    //public int attack => buffPower<Armor>();
    //public int away => buffPower<Armor>();
    //public int counterAttack => buffPower<Armor>();

    public bool Targetable => buffPower<Away>() == 0;

    public static int LevelCost(int level) {
        return level * 5;
    }

    public int NextLevelCost => LevelCost(level + 1);

    public int Experience {
        get {
            return experience;
        }
        set {
            experience = value;
            for (int i = 0; i < 100 & experience >= NextLevelCost; i++) {
                experience -= NextLevelCost;
                level++;
                skillPoints++;
            }
        }
    }

    private void OnValidate() {
        if (maxedHp && maxHp != hp) {
            maxHp = hp;
        }
    }

    public void Hit(Creature attacker, int damage = 1, AbilityEffect source = null, DamageType damageType = DamageType.Default) {
        var attack = new Attack(attacker, this, damage, source, damageType);
        GameLog.Message($"{attacker.Text()} attacks {Text()} by {damage} damage{(source != null ? $" with {source.Text(attacker)}" : "")}");

        IEnumerable<IAttackModifier> attackModifiers =
            (attacker?.buffs?.Where(b => b is IAttackModifier) ?? CollectionExtensions.Empty<Buff>())
            .Concat(buffs.Where(b => b is IAttackModifier))
            .Cast<IAttackModifier>()
            .Unique();

        foreach (var am in attackModifiers.OrderBy(am => am.Priority)) {
            am.ModifyAttack(attack);
            if (attack.interrupted) {
                break;
            }
        }

        GameManager.instance.ExecutePlannedActions();
    }

    public void LoseHp(int damage, AbilityEffect source = null, Creature attacker = null) {
        if (damage > hp) {
            damage = hp;
        }
        if (damage == 0) {
            return;
        }
        hp -= damage;
        GameLog.Message($"{Text()} lost {damage} hp{(source == null ? (attacker == null ? "" : $" by {attacker.Text()}") : $" by {source}")}");
        DeathCheck(source);
        GlobalEvents.instance.onLoseHp(this, damage, source);
    }

    public void Heal(int heal = 1) {
        if (!Alive) {
            return;
        }
        var oldhp = hp;
        hp += heal;
        HpCheck();
        var delta = hp - oldhp;
        if (delta == 0) {
            return;
        }
        GameLog.Message($"{name} healed by {delta}");
        DeathCheck();
    }

    public void HpCheck() {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void DeathCheck(AbilityEffect source = null) {
        if (hp <= 0) {
            GameManager.instance.PlanAction(() => Die(source));
        }
    }

    public void Die() {
        Die(null);
    }

    public virtual void Die(AbilityEffect source = null) {
        onDeath.Invoke(source);
        GlobalEvents.instance.onDeath(this, source);
        Destroy(gameObject);
    }

    public void Attack(Creature target) {
        Attack(target, damage);
    }

    public void Attack(Creature target, int damage) {
        beforeAttack(this, target, damage);
        target.Hit(this, damage);
        ApplyBuff<Stunned>(slow);
        afterAttack(this, target, damage);
    }

    public void UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
    }

    public virtual IPromise MakeMove() {
        if (!Battle.On) {
            return Promise.Resolved();
        }
        if (Dead) {
            return Promise.Resolved();
        }
        return TimeManager.Wait(0.25f).Then(() => {
            if (buffPower<Stunned>() > 0) {
                buff<Stunned>().Spend();
            } else if (buffPower<Away>() > 0) {
                buff<Away>().Spend();
            } else {
                TakeAction();
            }
            AfterMove();
        });
    }

    public virtual void AfterMove() {

        IEnumerable<IEndTurnModifier> endTurnModifiers =
            buffs.Where(b => b is IEndTurnModifier)
            .Cast<IEndTurnModifier>();

        foreach (var etm in endTurnModifiers.OrderBy(etm => etm.Priority)) {
            etm.OnTurnEnd();
        }

        GameManager.instance.ExecutePlannedActions();
    }

    public virtual void TakeAction() {
    }

    public virtual void Start() {
        GlobalEvents.instance.onBattleEnd += OnBattleEnd;
        OnSpawn();
    }

    public virtual void OnSpawn() {
        GlobalEvents.instance.onSpawn(this);
    }

    public void OnDestroy() {
        if (GlobalEvents.instance) {
            GlobalEvents.instance.onBattleEnd -= OnBattleEnd;
        }
    }

    protected virtual void OnBattleEnd(Battle battle) {
        buff<ProtectionUntilEndOfCombat>()?.Expire();
        buff<IncreasedAttack>()?.Expire();
        buff<Bubble>()?.Expire();
        buff<Away>()?.Expire();

        IEnumerable<IEndCombatModifier> endTurnModifiers =
            buffs.Where(b => b is IEndCombatModifier)
            .Cast<IEndCombatModifier>();

        foreach (var etm in endTurnModifiers.OrderBy(etm => etm.Priority)) {
            etm.OnCombatEnd();
        }
    }

    public virtual string Text() {
        return name;
    }

    public override string ToString() {
        return Text();
    }
}
