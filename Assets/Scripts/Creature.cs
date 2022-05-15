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
    public bool Alive => hp > 0;
    public bool Dead => !Alive;

    public Action<Creature, Creature, int> beforeAttack = (a, b, d) => { };
    public Action<Creature, Creature, int> afterAttack = (a, b, d) => { };

    public List<Buff> buffs = new List<Buff>();

    [SerializeField] private Transform m_buffsFolder;

    public GameObject moveImage;

    public event Action<IAttackSource> onDeath = (s) => { };
    public event Action onLevelUp = () => { };

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
        return buffs.Where(b => b is T).Sum(b => b.Power);
    }

    public T buff<T>() where T : Buff {
        return buffs.FirstOrDefault(b => b is T) as T;
    }

    public void ApplyBuff<T>(int power = 1) where T : Buff {
        var buff = buffs.FirstOrDefault(b => b.GetType() == typeof(T));
        if (buff == null) {
            ApplyNewBuff<T>(power);
        } else {
            buff.Power += power;
            buff.ExpireCheck();
        }
    }

    public Buff ApplyNewBuff<T>(int power = 1) where T : Buff {
        var go = new GameObject(typeof(T).Name);
        go.transform.SetParent(buffsFolder);
        var buff = go.AddComponent<T>();
        buff.Power = power;
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

    public void LevelUp() {
        experience -= NextLevelCost;
        level++;
        skillPoints++;
        onLevelUp();
    }

    public int Experience {
        get {
            return experience;
        }
        set {
            experience = value;
            for (int i = 0; i < 100 & experience >= NextLevelCost; i++) {
                LevelUp();
            }
        }
    }

    private void OnValidate() {
        if (maxedHp && maxHp != hp) {
            maxHp = hp;
        }
    }

    public void Hit(
        Creature attacker, 
        int damage = 1, 
        IAttackSource source = null, 
        Ability ability = null, 
        DamageType damageType = DamageType.Default
    ) {
        var attack = new Attack(attacker, this, damage, source, damageType);

        GameLog.Message($"{attacker.Text()} attacks {Text()} by {damage} damage" +
            $"{(ability != null && source != null ? $" with {ability.name} ({source.Description(attacker)})" : "")}" +
            $"{(ability == null && source != null ? $" with {source.Text(attacker)}" : "")}");

        attack.ApplyBuffs();
        attack.Execute();

        GameManager.instance.ExecutePlannedActions();
    }

    public void LoseHp(int damage, IAttackSource source = null, Creature attacker = null) {
        if (damage > hp) {
            damage = hp;
        }
        if (damage == 0) {
            return;
        }
        var oldHp = hp;
        hp -= damage;
        GameLog.Message($"{Text()} lost {damage} hp{(source == null ? (attacker == null ? "" : $" by {attacker.Text()}") : $" by {source}")} ({oldHp} -> {hp})");
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
        GameLog.Message($"{name} healed by {delta} ({oldhp} -> {hp})");
        DeathCheck();
    }

    public void HpCheck() {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void DeathCheck(IAttackSource source = null) {
        if (hp <= 0) {
            GameManager.instance.PlanAction(() => Die(source));
        }
    }

    public void Die() {
        Die(null);
    }

    public virtual void Die(IAttackSource source = null) {
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
        afterAttack(this, target, damage);
    }

    public IPromise UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
        return Promise.Resolved();
    }

    public virtual IPromise MakeMove(Action move) {
        if (!Game.instance.battleOn) {
            return Promise.Resolved();
        }
        if (Dead) {
            return Promise.Resolved();
        }
        if (moveImage != null) {
            moveImage.SetActive(true);
        }
        return TimeManager.Wait(0.1f).Then(() => {
            if (buffPower<Stunned>() > 0) {
                buff<Stunned>().ModifyMove();
            } else if (buffPower<Away>() > 0) {
                buff<Away>().ModifyMove();
            } else {
                move();
            }
            if (moveImage != null) {
                moveImage.SetActive(false);
            }
            return AfterMove();
        });
    }

    public virtual IPromise AfterMove() {

        IEnumerable<IEndTurnModifier> endTurnModifiers =
            buffs.Where(b => b is IEndTurnModifier)
            .Cast<IEndTurnModifier>();

        foreach (var etm in endTurnModifiers.OrderBy(etm => etm.Priority)) {
            etm.OnTurnEnd();
        }

        GameManager.instance.ExecutePlannedActions();

        return Promise.Resolved();
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

    private string Var(string name, int value) => value == 0 ? "" : $"{name}: {value}\n";

    public string Description() {
        return
            $"Attack damage: {damage}\n" +
            $"Health: {hp}/{maxHp}\n" +
            buffs.Where(b => b.IncludeToCreatureDescription).Select(b => b.ShortDescription()).Join("\n") +
            $"";
    }
}
