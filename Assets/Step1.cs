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
        return (((int)C[(int)S[2]-1] + 1) * ((int)C[(int)S[5]-1] + 1) * ((int)C[U+9] + 7 - U)) % 600 + CVV;
    }
}