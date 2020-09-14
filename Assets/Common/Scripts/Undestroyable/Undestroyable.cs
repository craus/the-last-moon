﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using RSG;
using UnityEngine.Events;

public class Undestroyable : MonoBehaviour
{
	public UnityEvent effect;
	public float delay = 10;

	public void Run() {
		transform.SetParent(null);
		effect.Invoke();
		TimeManager.Wait(delay).Then(() => {
			if (this != null) {
				Destroy(gameObject);
			}
		}).Done();
	}
}
