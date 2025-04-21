using System;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Dictionary<CostState, Color> CostColors = new Dictionary<CostState, Color>()
    {
        { CostState.Default, new Color(1, 244f/255f, 44f/255f) },
        { CostState.NoBid, new Color(0, 11f/255f, 229f/255f) },
        { CostState.Player, new Color(0, 1, 33f/255f) },
        { CostState.Module, new Color(195f/255, 0, 69f/255f) }
    };
    public static string IntToText(int Int, int MaxChar)
    {
        string output = Int.ToString();
        while (output.Length < MaxChar)
        {
            output = '0' + output;
        }
        return output;
    }
    public static string LongToText(long Long, int MaxChar)
    {
        string output = Long.ToString();
        while (output.Length < MaxChar)
        {
            output = '0' + output;
        }
        return output;
    }
}