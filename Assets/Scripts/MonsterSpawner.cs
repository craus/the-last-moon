﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Monster
{
    public Monster sample;
    public Transform parent;
    public float mana;
    public float manaPerTurn = 3;

    public override void TakeAction() {
        base.TakeAction();
        DebugManager.LogFormat("MonsterSpawner TakeAction");

        mana += manaPerTurn;

        if (Rand.rndEvents(0.2f, (int)mana) >= 1) {
            SpendMana();
        }
    }

    void SpendMana() {
        for (int i = 0; i < 100 && mana > 0; i++) {
            SpawnMonster();
        }
    }

    Monster NewMonster() {
        var m = Instantiate(sample);
        m.transform.SetParent(parent);
        return m;
    }

    void SpawnMonster() {
        --mana;
        var m = NewMonster();
        for (int i = 0; i < 100 && mana > 0; i++) {
            if (Rand.rndEvent(0.1f)) {
                break;
            }
            BuffMonster(m);
        }
    }

    void BuffMonster(Monster m) {
        if (Rand.rndEvent(0.5f)) {
            m.hp += 2;
            m.maxHp += 2;
            mana -= 1;
        } else {
            m.damage += 1;
            mana -= 1;
        }
    }
}
