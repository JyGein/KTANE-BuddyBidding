
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using System.Security.AccessControl;

//HI :3 gl with modding


// this template here need to be the EXACT same thing as yo module type
public class BuddyBidding : MonoBehaviour {
     // might aswell name the script file the same thing

    // Modding Tutorial by Deaf: https://www.youtube.com/watch?v=YobuGSBl3i0

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;
    public CreditCard creditCard;
    public BiddingPad biddingPad;

    public KMSelectable LeftButton;
    public KMSelectable RightButton;
    public List<KMSelectable> KeypadNumbers;
    public KMSelectable SubmitButton;
    public KMSelectable DeleteButton;

    public List<Texture> Items;
    public List<Texture> Buddies;
    private Texture TodaysBuddy;

    string ModuleName;
    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    private bool IsOpen = false;

#pragma warning disable IDE0051
    void Awake ()
    { //Shit that happens before Start
#pragma warning restore IDE0051
        ModuleName = Module.ModuleDisplayName;
        ModuleId = ModuleIdCounter++;
        Module.OnActivate += Activate;
        TodaysBuddy = Buddies[Rnd.Range(0, Buddies.Count)];
        biddingPad.SetDisplay(TodaysBuddy);
        creditCard.Log += Log;
        foreach(KMSelectable child in GetComponent<KMSelectable>().Children)
        {
            child.OnInteract += delegate () { child.AddInteractionPunch(); return false; };
        }
        for(int i = 0; i <= 9; i++)
        {
            int dummy = i;
            KeypadNumbers[i].OnInteract += delegate () { HandleNumber(dummy); return false; };
        }
        GetComponent<KMSelectable>().OnFocus += delegate () { IsSelected = true; };
        GetComponent<KMSelectable>().OnDefocus += delegate () { IsSelected = false; };
    }

    void Activate () 
    { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }
#pragma warning disable IDE0051
    void Start ()
    { //Shit
#pragma warning restore IDE0051

    }

    private bool IsSelected = false;
#pragma warning disable IDE0051
    void Update ()
    { //Shit that happens at any point after initialization
#pragma warning restore IDE0051
        if (IsSelected)
        {
            // numpad handler
            if (Input.GetKeyDown(KeyCode.Keypad0)) HandleNumber(0);
            if (Input.GetKeyDown(KeyCode.Keypad1)) HandleNumber(1);
            if (Input.GetKeyDown(KeyCode.Keypad2)) HandleNumber(2);
            if (Input.GetKeyDown(KeyCode.Keypad3)) HandleNumber(3);
            if (Input.GetKeyDown(KeyCode.Keypad4)) HandleNumber(4);
            if (Input.GetKeyDown(KeyCode.Keypad5)) HandleNumber(5);
            if (Input.GetKeyDown(KeyCode.Keypad6)) HandleNumber(6);
            if (Input.GetKeyDown(KeyCode.Keypad7)) HandleNumber(7);
            if (Input.GetKeyDown(KeyCode.Keypad8)) HandleNumber(8);
            if (Input.GetKeyDown(KeyCode.Keypad9)) HandleNumber(9);
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) HandleSubmit();
            if (Input.GetKeyDown(KeyCode.Backspace)) HandleDelete();
            if (Input.GetKeyDown(KeyCode.RightArrow)) HandleRight();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) HandleLeft();
        }
    }

    void HandleNumber(int digit)
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        if (!IsOpen) return;
    }

    void HandleSubmit()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
    }

    void HandleDelete()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        if (!IsOpen) return;
    }

    void HandleLeft()
    {
        if (!IsOpen)
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
            return;
        };
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, gameObject.transform);
    }

    void HandleRight()
    {
        if (!IsOpen)
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
            return;
        };
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, gameObject.transform);
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

    public void Log (string message) 
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
