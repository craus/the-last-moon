using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Counter : IntValueProvider {
	[SerializeField] private int value;

	public override int Value => value;

	public void Increment() {
		value += 1;
	}
	public void Decrement() {
		value -= 1;
	}
	public void Reset() {
		value = 0;
	}
}
