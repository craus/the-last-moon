using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Texts
{
    public const string G = "$";
    public const string SP = "SP";
    public const string DKIG = "∯₮";

    public static string sp(int amount) {
        return $"{amount} {SP}";
    }

    public static string skillPoints(int amount) {
        return $"{amount} skill points";
    }
}
