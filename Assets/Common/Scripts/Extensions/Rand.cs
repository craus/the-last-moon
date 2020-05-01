using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Rand
{
	public static bool rndEvent(double p) {
		return UnityEngine.Random.value < p;
	}

	public static int rndEvents(double p, int cnt) {
		int result = 0;
		for (int i = 0; i < cnt; i++) {
			if (rndEvent(p)) {
				++result;
			}
		}
		return result;
	}

	public static int d0(int cnt, int edges) {
		int result = 0;
		for (int i = 0; i < cnt; i++) {
			result += UnityEngine.Random.Range(0, edges);
		}
		return result;
	}

	public static int d(int cnt, int edges) {
		return d0(cnt, edges) + cnt;
	}

	public static T rnd<T>(T[,] matrix) {
		return matrix[UnityEngine.Random.Range(0, matrix.GetLength(0)), UnityEngine.Random.Range(0, matrix.GetLength(1))];
	}

	public static T rnd<T>(T[,] matrix, Func<T, bool> condition) {
		for (int i = 0; i < 100; i++) {
			var result = rnd(matrix);
			if (condition(result)) {
				return result;
			}
		}
		return rnd(matrix);
	}
}