using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : Monster
{
    public Monster sample;
    public Transform parent;
    public float mana;
    public float manaPerGameDay = 0.5f;
    public float manaPerTurn = 2.15f;
    public float manaPerTurn2 = 0.0015f;

    public override void Start() {
        base.Start();
    }

    public override void TakeAction() {
        base.TakeAction();
        DebugManager.LogFormat("MonsterSpawner TakeAction");

        mana += manaPerTurn;
        manaPerTurn += manaPerTurn2;

        if (Rand.rndEvents(0.2f, (int)mana) >= 1) {
            SpendMana();
        }
    }

    public void SpendMana() {
        for (int i = 0; i < 100 && mana > 0; i++) {
            SpawnMonster();
        }
        GameLog.Message(
            "Monsters: {0}".i(
                FindObjectsOfType<Monster>()
                    .Where(m => !(m is MonsterSpawner))
                    .ExtToString(
                        elementToString: m => m.Text(),
                        format: "{0}"
                    )
            )
        );
        GameLog.LogBattleRound();
    }

    Monster NewMonster() {
        var m = Instantiate(sample, parent);
        return m;
    }

    void SpawnMonster() {
        --mana;
        var m = NewMonster();
        for (int i = 0; i < 100 && mana > 0; i++) {
            if (Rand.rndEvent(0.05f)) {
                break;
            }
            BuffMonster(m);
        }
    }

    void BuffMonster(Monster m) {
        if (Rand.rndEvent(0.07f)) {
            m.bubbles++;
            mana -= 1.3f;
            return;
        }
        if (Rand.rndEvent(0.1f) && m.hp - 1 > m.regeneration) {
            m.regeneration += 1;
            mana -= 1;
            return;
        }
        if (Rand.rndEvent(0.1f / (1+m.away))) {
            m.away += 1;
            mana += 1f / (1+m.away);
            return;
        }
        if (Rand.rndEvent(0.1f)) {
            m.armor += 1;
            mana -= 1;
            return;
        }
        if (Rand.rndEvent(0.1f) && m.hp - 1 > -m.armor) {
            m.armor -= 1;
            mana += 1;
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
}
