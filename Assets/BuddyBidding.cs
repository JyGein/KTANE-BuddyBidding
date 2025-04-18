
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

//HI :3 gl with modding


// this template here need to be the EXACT same thing as yo module type
public class BuddyBidding : MonoBehaviour {
     // might aswell name the script file the same thing

    // Modding Tutorial by Deaf: https://www.youtube.com/watch?v=YobuGSBl3i0

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    string ModuleName;
    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

#pragma warning disable IDE0051
    void Awake ()
    { //Shit that happens before Start
#pragma warning restore IDE0051
        ModuleName = Module.ModuleDisplayName;
        ModuleId = ModuleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;
        /*
         * How to make buttons work:
         * 
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };

        //keypadPress() and buttonPress() you have to make yourself and should just be what happens when you press a button. (deaf goes through it probably)
    }

    void Activate () 
    { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }
#pragma warning disable IDE0051
    void Start ()
    { //Shit
#pragma warning restore IDE0051

    }

#pragma warning disable IDE0051
    void Update ()
    { //Shit that happens at any point after initialization
#pragma warning restore IDE0051

    }

    void Solve (string message) 
    { //Call this method when you want the module to solve
        Module.HandlePass();
        Log(message);
        ModuleSolved = true;
    }

    void Strike (string message)
    { //Call this method when you want ot module to strike
        Module.HandleStrike();
        Log(message);
    }

    void Log (string message) 
    {
        Debug.Log($"[{ModuleName} #{ModuleId}] {message}");
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    // Twitch Plays (TP) documentation: https://github.com/samfundev/KtaneTwitchPlays/wiki/External-Mod-Module-Support

#pragma warning disable IDE0051
    IEnumerator ProcessTwitchCommand (string Command)
    {
#pragma warning restore IDE0051
        yield return null;
    }

#pragma warning disable IDE0051
    IEnumerator TwitchHandleForcedSolve ()
    {
#pragma warning restore IDE0051
        yield return null;
    }
}
