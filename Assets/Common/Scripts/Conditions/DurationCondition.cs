using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Common
{
    public class DurationCondition : BoolValueProvider
    {
        [SerializeField] private BoolValueProvider condition;
        [SerializeField] private float duration;

        [SerializeField] [ReadOnly] private float lastFalseTime;

        public override bool Value => lastFalseTime < TimeManager.Time() - duration;

        protected override void Update()
        {
            if (!condition.Value)
            {
                lastFalseTime = TimeManager.Time();
            }
        }
    }
}
