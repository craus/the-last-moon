using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public static class CollectionExtensions
{
    public static IEnumerable<T> Single<T>(this T element)
    {
        yield return element;
    }

    public static IEnumerable<T> Empty<T>() {
        yield break;
    }

    public static int IndexOfMin<T>(this IList<T> list, Func<T, float> criteria)
    {
        int answer = 0;
        for (int i = 0; i < list.Count(); i++)
        {
            if (criteria(list[i]) < criteria(list[answer]))
            {
                answer = i;
            }
        }
        return answer;
    }
}