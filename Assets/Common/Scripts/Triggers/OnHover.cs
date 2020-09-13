using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common
{
    public class OnHover : Trigger, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData) {
            DebugManager.LogFormat("OnPointerEnter");
            Run();
        }
    }
}
