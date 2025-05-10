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
            output += IntParseA10Z35(number[i]);
        }
        return output;
    }
    public static int IntParseA10Z35(char c)
    {
        int i;
        if(!int.TryParse(c.ToString(), out i))
        {
            return c - 55;
        }
        return i;
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
    public static bool ShareALetter(string first, string second)
    {
        foreach(char c in first)
        {
            foreach(char ch in second)
            {
                if (c == ch) return true;
            }
        }
        return false;
    }
    public static int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}

public struct BidInfo {
    public readonly Item item;
    public readonly float timeLeft;
    public int currentBid;
    public readonly CostState costState;
    public BidInfo(Item _item, float _timeLeft, int _currentBid, CostState _costState )
    {
        item = _item;
        timeLeft = _timeLeft;
        currentBid = _currentBid;
        costState = _costState;
    }
}
