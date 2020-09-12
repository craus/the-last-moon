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

    public Creature owner {
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

    public void Spend(int delta = 1) {
        power = Mathf.Clamp(power - delta, 0, int.MaxValue);
        ExpireCheck();
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
