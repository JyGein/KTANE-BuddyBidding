using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public static class Step1 {
    public static int GenerateBalance(BuddyBidding Module, KMBombInfo Bomb) {
        string C = Module.creditCard.CreditCardNumberStr;
        string S = Bomb.GetSerialNumber();
        int CVV = Module.creditCard.CVVNumber;
        int U = S.Distinct().Count();
        return ((int.Parse(C[int.Parse(S[2].ToString())].ToString()) + 1) * (int.Parse(C[int.Parse(S[5].ToString())].ToString()) + 1) * (int.Parse(C[U+9].ToString()) + 7 - U) + Util.SumPosition(Bomb.GetSerialNumber(), false)) % 600 + CVV;
    }
}