using RSG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : Monster
{
    public Monster sample;
    public Transform parent;
    public float mana;
    public float manaPerTurn;

    public override void Start() {
        base.Start();
    }

    public override void TakeAction() {
        base.TakeAction();
        DebugManager.LogFormat("MonsterSpawner TakeAction");

        mana += manaPerTurn;
        manaPerTurn += Game.instance.spawnerManaPerTurn2;

        if (Rand.rndEvents(0.2f, (int)mana) >= 1) {
            SpendMana();
        }
    }

    public bool TooManyMonsters(int x) {
        return Rand.rndEvent(new double[] { 0, 0.15f, 0.5f, 0.8f, 0.9f, 0.95f, 0.98f, 1, 1 }[x]);
    }

    public int MonstersCount() {
        var result = 1;
        for (int i = 0; i < 100 && !TooManyMonsters(result); i++) {
            result++;
        }
        return result;
    }

    public void SpendMana() {
        int cnt = MonstersCount();

        float[] monsterMana = Rand.rndSplit(mana, cnt);

        for (int i = 0; i < cnt; i++) {
            SpawnMonster(monsterMana[i]);
        }

        GameLog.Message($"Battle started - Day {Game.instance.day}");
        GameLog.LogMonsters();

        Game.instance.battle.PlanStartBattle();
    }

    Monster NewMonster() {
        var m = Instantiate(sample, parent);
        m.name = S.monsterNames.rnd();
        return m;
    }

    void SpawnMonster(float mana) {
        mana -= 2;
        var delta = Mathf.Sqrt(mana + 4);
        var manaForBuffs = Rand.Rnd(
            Mathf.Clamp(mana, 0, float.PositiveInfinity),
            Mathf.Clamp(mana + delta, 0, float.PositiveInfinity)
        );
        var manaForDebuffs = mana - manaForBuffs;
        var m = NewMonster();
        for (int i = 0; i < 100 && manaForBuffs > 0; i++) {
            BuffMonster(m, ref manaForBuffs);
        }
        for (int i = 0; i < 100 && manaForDebuffs < 0; i++) {
            DebuffMonster(m, ref manaForDebuffs);
        }
    }

    void BuffMonster(Monster m, ref float mana) {
        if (Rand.rndEvent(0.07f)) {
            m.ApplyBuff<Bubble>();
            mana -= 1.3f;
            return;
        }
        if (Rand.rndEvent(0.1f) && m.hp - 1 > m.buffPower<Regeneration>()) {
            m.ApplyBuff<Regeneration>(1);
            mana -= 1;
            return;
        }
        if (Rand.rndEvent(0.1f)) {
            m.ApplyBuff<Armor>(1);
            mana -= 1;
            return;
        }
        if (Rand.rndEvent(0.1f)) {
            m.ApplyBuff<CounterAttack>(1);
            mana -= 1;
            return;
        }
        if (Rand.rndEvent(0.7f)) {
            m.hp += 1;
            m.maxHp += 1;
            mana -= 0.5f;
            return;
        }
        m.damage += 1;
        mana -= 1;
    }

    void DebuffMonster(Monster m, ref float mana) {
        if (Rand.rndEvent(0.1f / (1 + m.buffPower<Away>()))) {
            m.ApplyBuff<Away>(1);
            mana += 1f / (1 + m.buffPower<Away>());
            return;
        }
        if (Rand.rndEvent(0.1f) && m.hp - 1 > -m.buffPower<Armor>()) {
            m.ApplyBuff<Armor>(-1);
            mana += 1;
            return;
        }
        if (Rand.rndEvent(0.5f)) {
            m.slow++;
            mana += 1;
        }
        m.ApplyBuff<Stunned>(1);
        mana += 1;
    }
}
