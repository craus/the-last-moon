using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onShow;
    public UnityEvent onHide;

    public float delay = 0.4f;
    public bool hovered = false;
    public bool shown = false;

    public float lastEnterTime = float.NegativeInfinity;

    public void OnPointerEnter(PointerEventData eventData) {
        lastEnterTime = Time.time;
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovered = false;
    }

    public void Start() {
        Hide();
    }

    public void Hide() {
        shown = false;
        onHide.Invoke();
    }

    public void Show() {
        shown = true;
        onShow.Invoke();
    }

    public void Update() {
        if (!shown && hovered && Time.time > lastEnterTime + delay) {
            Show();
        }
        if (shown && !hovered) {
            Hide();
        }
    }
}
