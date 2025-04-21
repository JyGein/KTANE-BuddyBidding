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
    public static int SumPosition(string number, bool Even) {
        int output = 0;
        for (int i = Even ? 1 : 0; i < number.Length; i += 2) {
            output += (int)number[i];
        }
        return output;
    }
    public static int DigitalRoot(int number) {
        int output = number % 9;
        output = output == 0 ? 9 : output;
        return output;
    }
    public static int DigitalRoot(long number) {
        int output = (int)(number % 9);
        output = output == 0 ? 9 : output;
        return output;
    }
}

public struct BidInfo {
    public readonly Item item;
    public readonly float timeLeft;
    public readonly int currentBid;
    public readonly CostState costState;
}
