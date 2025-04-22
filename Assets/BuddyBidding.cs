using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using System.Security.AccessControl;
using Events;
using Assets.Scripts.Records;
using Assets.Scripts.Utility;

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
    public Buddy TodaysBuddy;

    string ModuleName;
    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved = false;

    public bool IsOpen = false;
    public List<Auction> Auctions = new List<Auction>();
    public int CurrentAuction = 0;
    public List<Item> ChosenItems = new List<Item>();
    public int PlayerBalance = 0;
    public bool ActuallyDonating = false;
    public int LostWantedItems = 0;
    public int BoughtWantedItems = 0;
    public int CurrentPlayerInput = 0;
    public int DonataedAmount = 0;
    public List<ModuleBot> WantedItemBots = new List<ModuleBot>();
    public int WantedItemMaxBidRunningTotal = 0;
    public int MaxofBotMaxes = 0;

#pragma warning disable IDE0051
    void Awake ()
    { //Shit that happens before Start
#pragma warning restore IDE0051
        ModuleName = Module.ModuleDisplayName;
        ModuleId = ModuleIdCounter++;
        Module.OnActivate += Activate;
        TodaysBuddy = (Buddy)Rnd.Range(0, Buddies.Count);
        biddingPad.SetDisplay(Buddies[(int)TodaysBuddy]);
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
        LeftButton.OnInteract += delegate () { HandleLeft(); return false; };
        RightButton.OnInteract += delegate () { HandleRight(); return false; };
        SubmitButton.OnInteract += delegate () { HandleSubmit(); return false; };
        DeleteButton.OnInteract += delegate () { HandleDelete(); return false; };
        GetComponent<KMSelectable>().OnFocus += delegate () { IsSelected = true; };
        GetComponent<KMSelectable>().OnDefocus += delegate () { IsSelected = false; };
        Auctions.Add(new Auction { Item = Item.Buddy });
    }

    void Activate () 
    { //Shit that should happen when the bomb arrives (factory)/Lights turn on

    }
#pragma warning disable IDE0051
    void Start ()
    { //Shit
#pragma warning restore IDE0051
        PlayerBalance = Step1.GenerateBalance(this, Bomb);
        MaxofBotMaxes = PlayerBalance - 30 - PlayerBalance / 100;
        Log($"Your Balance: {PlayerBalance}");
        ChosenItems = Step2.GenerateItems(this, Bomb);
        if (ChosenItems[0] == Item.Buddy)
        {
            Log("Unicorn met, donate your balance to Buddy to solve.");
            ActuallyDonating = true;
        }
        else
        {
            Log("Wanted Items:");
            foreach (Item item in ChosenItems)
            {
                Log($"{item}");
                int BotNum = Rnd.Range(0, 3);
                List<ModuleBot> NewBots = new List<ModuleBot>();
                int ThisMaxBotsBid = 0;
                for (int i = 0; i <= BotNum; i++)
                {
                    int maxBid = Rnd.Range(50, MaxofBotMaxes - WantedItemMaxBidRunningTotal);
                    ModuleBot NewBot = new ModuleBot { MaxBid = maxBid };
                    NewBots.Add(NewBot);
                    WantedItemBots.Add(NewBot);
                    ThisMaxBotsBid = ThisMaxBotsBid > maxBid ? ThisMaxBotsBid : maxBid;
                }
                WantedItemMaxBidRunningTotal += ThisMaxBotsBid;
                Auctions.Add(new Auction { Item = item, ModuleBidders = NewBots });
            }
            AttemptToMaxoutMaxBids();
        }
        while (Auctions.Count < 7)
        {
            Item thisOnesItem = (Item)Rnd.Range(0, 10);
            while (Auctions.Select((a) => a.Item).Contains(thisOnesItem)) thisOnesItem = (Item)Rnd.Range(0, 10);
            List<ModuleBot> NewBots = new List<ModuleBot>();
            int BotNum = Rnd.Range(0, 3);
            for (int i = 0; i <= BotNum; i++)
            {
                int maxBid = Rnd.Range(50, 400);
                NewBots.Add(new ModuleBot { MaxBid = maxBid });
            }
            Auctions.Add(new Auction { Item = thisOnesItem, ModuleBidders = NewBots });
        }
        Auctions = Auctions.Shuffle();
    }

    private bool IsSelected = false;
#pragma warning disable IDE0051
    void Update ()
    { //Shit that happens at any point after initialization
#pragma warning restore IDE0051
        if (IsSelected)
        {
            for(int i = 0; i <= 9; i++) {
                if(Input.GetKeyDown(KeyCode.Keypad0 + i)) {
                  HandleNumber(i);
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) HandleSubmit();
            if (Input.GetKeyDown(KeyCode.Backspace)) HandleDelete();
            if (Input.GetKeyDown(KeyCode.RightArrow)) HandleRight();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) HandleLeft();
        }
        if (IsOpen && !ModuleSolved)
        {
            foreach (Auction auction in Auctions)
            {
                auction.Update(this);
            }
            UpdateDisplay();
        }
        print($"{CurrentPlayerInput}");
        print($"{CurrentAuction}");
    }

    void UpdateDisplay()
    {
        BidInfo selectedBidInfo = Auctions[CurrentAuction].GetBidInfo();
        if (CurrentPlayerInput > 0) selectedBidInfo.currentBid = CurrentPlayerInput;
        biddingPad.UpdateDisplay(selectedBidInfo);
    }

    void AttemptToMaxoutMaxBids()
    {
        if (Auctions.Where((a) => a.ModuleBidders.Count > 0 && ChosenItems.Contains(a.Item)).Count() >= 3)
        {
            Auction SelectedAuction = Auctions[Rnd.Range(1, 4)];
            ModuleBot SelectedBot = SelectedAuction.ModuleBidders.OrderByDescending(b => b.MaxBid).ToList()[0];
            SelectedBot.MaxBid += MaxofBotMaxes - WantedItemMaxBidRunningTotal;
            WantedItemMaxBidRunningTotal = MaxofBotMaxes;
        }
    }

    void HandleNumber(int digit)
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        if (!IsOpen) return;
        if (CurrentPlayerInput.ToString().Count() > 3) return;
        CurrentPlayerInput = int.Parse(CurrentPlayerInput.ToString() + digit.ToString());
    }

    void HandleSubmit()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        if (!IsOpen) {
            IsOpen = true;
            foreach(Auction auction in Auctions)
            {
                auction.Start(this);
            }
            return;
        }
        Auction SelectedAuction = Auctions[CurrentAuction];
        if (SelectedAuction.Item == Item.Buddy)
        {
            if (ActuallyDonating) 
            {
                if (CurrentPlayerInput + DonataedAmount > PlayerBalance) Strike("You donated too much money! Strike Issued!");
                else
                {
                    DonataedAmount += CurrentPlayerInput;
                    if (DonataedAmount >= PlayerBalance)
                    {
                        Solve("Buddy thanks you for your donation! Module Solved!");
                    }
                }
            }
            CurrentPlayerInput = 0;
            return;
        }
        if (SelectedAuction.CurrentBid < CurrentPlayerInput)
        {
            if (!ChosenItems.Contains(SelectedAuction.Item) && CurrentPlayerInput > 50)
            {
                int diff = SelectedAuction.PlayerBid >= 50 ? CurrentPlayerInput - SelectedAuction.PlayerBid : CurrentPlayerInput - 50;
                if (WantedItemBots.Count() > 0)
                {
                    WantedItemBots.Shuffle()[0].MaxBid -= diff;
                    WantedItemMaxBidRunningTotal -= diff;
                    MaxofBotMaxes -= diff;
                }
            }
            SelectedAuction.PlayerBid = CurrentPlayerInput;
            if (SelectedAuction.ModuleBidders.Count() <= 0)
            {
                int maxBid = Rnd.Range(50, ChosenItems.Contains(SelectedAuction.Item) ? MaxofBotMaxes - WantedItemMaxBidRunningTotal : 400);
                ModuleBot NewBot = new ModuleBot { MaxBid = maxBid };
                SelectedAuction.ModuleBidders.Add(NewBot);
                if (ChosenItems.Contains(SelectedAuction.Item))
                {
                    WantedItemBots.Add(NewBot);
                    WantedItemMaxBidRunningTotal += maxBid;
                    AttemptToMaxoutMaxBids();
                }
            }
            SelectedAuction.OnBidPlaced();
        }
        CurrentPlayerInput = 0;
        return;
    }

    void HandleDelete()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
        if (!IsOpen) return;
        if (CurrentPlayerInput.ToString().Count() <= 0) return;
        CurrentPlayerInput = int.Parse(CurrentPlayerInput.ToString().Substring(0, CurrentPlayerInput.ToString().Count()-1));
    }

    void HandleLeft()
    {
        if (!IsOpen)
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
            return;
        };
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, gameObject.transform);
        CurrentAuction = Util.mod(CurrentAuction - 1, Auctions.Count());
        CurrentPlayerInput = 0;
        UpdateDisplay();
    }

    void HandleRight()
    {
        if (!IsOpen)
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, gameObject.transform);
            return;
        };
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, gameObject.transform);
        CurrentAuction = Util.mod(CurrentAuction + 1, Auctions.Count());
        CurrentPlayerInput = 0;
        UpdateDisplay();
    }

    public void SellItem(Auction auction)
    {
        bool IsChosenItem = ChosenItems.Contains(auction.Item);
        CostState Buyer = auction.GetCostState();
        if (Buyer == CostState.Module && IsChosenItem)
        {
            Strike($"You wanted the {auction.Item} but it was sold to someone else! Strike!!");
            LostWantedItems++;
            if (LostWantedItems >= 3)
            {
                Log("You lost all of your wanted items!! Goodbye!!");
                Detonate();
            }
        }
        if (Buyer == CostState.Player && !IsChosenItem)
        {
            Strike($"You didn't want the {auction.Item} and yet you bought it! Strike!!");
            //Change max bids of a wanted item auction accordingly
        }
        if (Buyer == CostState.Player && IsChosenItem)
        {
            BoughtWantedItems++;
            Log($"{auction.Item} sold to the defuser!");
        }
        if (BoughtWantedItems + LostWantedItems >= 3)
        {
            Solve("All wanted items have been sold! Module solved.");
        }
    }

    void Solve (string message) 
    {
        biddingPad.Timer.text = "";
        Module.HandlePass();
        Log(message);
        ModuleSolved = true;
    }

    void Strike (string message)
    {
        if (ModuleSolved) return;
        Module.HandleStrike();
        Log(message);
    }

    void Detonate ()
    {
        var bomb = GetComponentInParent<Bomb>();
        if (bomb && !bomb.HasDetonated)
        {
            float elapsed = bomb.GetTimer().TimeElapsed;

            var strikes = RecordManager.Instance.GetCurrentRecord().Strikes;

            strikes[strikes.Length - 1] = new StrikeSource
            {
                ComponentType = Assets.Scripts.Missions.ComponentTypeEnum.Mod,
                InteractionType = InteractionTypeEnum.PushButton,
                Time = elapsed,
                ComponentName = "Buddy Bidding Bidding you farewell you non-Buddy",
            };

            RecordManager.Instance.SetResult(GameResultEnum.ExplodedDueToStrikes, elapsed, SceneManager.Instance.GameplayState.GetElapsedRealTime());

            bomb.Detonate();
        }
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
