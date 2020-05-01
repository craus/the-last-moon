using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class ValueProvider<T> : AbstractValueProvider {
	public abstract T Value {
		get;
	}

	public event Action onChange;

	public void Changed() {
		if (onChange != null) {
			onChange.Invoke();
		}
	}

	[SerializeField] [ReadOnly] private T value;

	protected virtual void Update() {
#if UNITY_EDITOR
		value = Value;
#endif
	}
}
