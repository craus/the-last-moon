﻿using System;
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
    public int stunned = 0;
    public int regeneration = 0;
    public bool counterattackOn = false;
    public int slow = 0;
    public bool Alive => hp > 0;
    public bool Dead => !Alive;

    public Action<Creature, Creature, int> beforeAttack = (a, b, d) => { };
    public Action<Creature, Creature, int> afterAttack = (a, b, d) => { };

    public List<Buff> buffs = new List<Buff>();
    public Transform buffsFolder;

    public int buffPower<T>() {
        return buffs.Where(b => b is T).Sum(b => b.power);
    }

    public Buff buff<T>() {
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
        if (buffsFolder == null) {
            var bfgo = new GameObject("Buffs");
            bfgo.transform.SetParent(transform);
            buffsFolder = bfgo.transform;
        }
        go.transform.SetParent(buffsFolder);
        var buff = go.AddComponent<T>();
        buff.power = power;
        buff.owner = this;
        buffs.Add(buff);
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

        IEnumerable<IAttackModifier> attackModifiers =
            attacker.buffs.Where(b => b is IAttackModifier)
            .Concat(buffs.Where(b => b is IAttackModifier))
            .Cast<IAttackModifier>();

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

    public void Poison(int poison = 1) {
        regeneration -= poison;
    }

    public void Heal(int heal = 1) {
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

    public void Stun(int stun = 1) {
        stunned += stun;
    }

    public void Afterburner(int power = 1) {
        stunned -= power;
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
        GlobalEvents.instance.onDeath(this, source);
        Destroy(gameObject);
    }

    public void Attack(Creature target) {
        Attack(target, damage);
    }

    public void Attack(Creature target, int damage) {
        beforeAttack(this, target, damage);
        target.Hit(this, damage);
        stunned += slow;
        afterAttack(this, target, damage);
    }

    public void UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
    }

    public virtual void MakeMove() {
        if (!Battle.On) {
            return;
        }
        if (Dead) {
            return;
        }
        if (stunned > 0) {
            stunned--;
        } else if (buffPower<Away>() > 0) {
            buff<Away>().Spend();
        } else {
            TakeAction();
        }
        AfterMove();
    }

    public virtual void AfterMove() {
        Heal(regeneration);
    }

    public virtual void TakeAction() {
    }

    public virtual void Start() {
        GlobalEvents.instance.onBattleEnd += OnBattleEnd;
        OnSpawn();
    }

    public virtual void OnSpawn() {
        GlobalEvents.instance.onSpawn(this);
        ApplyBuff<LoseHpFromAttack>();
    }

    public void OnDestroy() {
        if (GlobalEvents.instance) {
            GlobalEvents.instance.onBattleEnd -= OnBattleEnd;
        }
    }

    protected virtual void OnBattleEnd(Battle b) {
        buff<ProtectionUntilEndOfCombat>()?.Expire();
        buff<IncreasedAttack>()?.Expire();
        buff<Bubble>()?.Expire();
        buff<Away>()?.Expire();
    }

    public virtual string Text() {
        return name;
    }
}
