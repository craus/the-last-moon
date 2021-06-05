using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Buff : MonoBehaviour
{
    [SerializeField] private Creature m_owner;

    public virtual Creature owner {
        get {
            return m_owner;
        }
        set {
            if (m_owner != null) {
                m_owner.buffs.Remove(this);
            }
            m_owner = value;
            if (m_owner != null) {
                m_owner.buffs.Add(this);
            }
        }
    }

    public int power = 1;

    public Buff ApplyNew(Creature target) {
        var buff = Instantiate(this, target.buffsFolder);
        buff.owner = target;
        return buff;
    }

    public void Apply(Creature target) {
        var buff = target.buffs.FirstOrDefault(b => b.GetType() == GetType());
        if (buff == null) {
            ApplyNew(target);
        } else {
            buff.power += power;
        }
    }

    public void Spend() => Spend(1);

    public void Spend(int delta = 1) {
        var oldPower = power;
        power = Mathf.Clamp(power - delta, 0, int.MaxValue);

        LogSpend(delta, oldPower);
        ExpireCheck();
    }

    public virtual void LogSpend(int delta, int oldPower) {
        GameLog.Message($"{owner.Text()} spends {delta} {Name} ({oldPower} -> {power})");
    }

    protected virtual void ModifyAttackDamage(Attack attack, int delta) {
        var old = attack.damage;
        attack.damage += delta;
        attack.Does(() => {
            LogAttackDamageModification(delta, old, attack);
        });
    }

    protected virtual void LogAttackDamageModification(int delta, int old, Attack attack) {
        GameLog.Message($"{Name} modifies attack damage by {delta} ({old} -> {attack.damage})");
    }

    public void ExpireCheck() {
        if (power == 0) {
            Expire();
        }
    }

    public void Expire() {
        owner = null;
        Destroy(gameObject);
    }

    public void OnDestroy() {
        owner = null;
    }

    public void UntilEndOfBattle() {
    }

    public virtual bool IncludeToCreatureDescription => true;

    public virtual string Name => GetType().Name;

    public virtual string Description() {
        return $"{Name}: {power}";
    }

    public virtual string ShortDescription() {
        return Description();
    }

    public virtual string Text() {
        return "";
    }

    public void Update() {
        if (Extensions.InEditMode()) {
#if UNITY_EDITOR
            if (name != GetType().Name) {
                name = GetType().Name;
                EditorUtility.SetDirty(gameObject);
            }
#endif
        }
    }
}
