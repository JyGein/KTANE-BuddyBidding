using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Auction
{
    public const int minBid = 25;
    public List<ModuleBot> ModuleBidders = new List<ModuleBot>();
    public Item Item;
    public int PlayerBid = 0;
    public float Timer = 0;
    public bool Sold = false;
    public int CurrentBid
    {
        get
        {
            CostState currentState = GetCostState();
            switch (currentState)
            {
                case CostState.Player:
                    return PlayerBid;
                case CostState.NoBid:
                    return minBid;
                case CostState.Module:
                    return GetMaxCurrentBotBid();
                default:
                    return minBid;
            }
        }
    }
    public CostState GetCostState()
    {
        if (Item == Item.Buddy) return CostState.Default; 
        int maxBotBid = GetMaxCurrentBotBid();
        if (maxBotBid == 0 && PlayerBid == 0) return CostState.NoBid;
        if (maxBotBid > PlayerBid) return CostState.Module;
        return CostState.Player;
    }
    public int GetMaxCurrentBotBid()
    {
        int output = 0;
        foreach(ModuleBot ModBidder in ModuleBidders)
        {
            output = ModBidder.CurrentBid > output ? ModBidder.CurrentBid : output;
        }
        return output;
    }
    public void Start(BuddyBidding Module)
    {
        if (Item == Item.Buddy) return;
        Timer = 60;
        foreach(ModuleBot bot in ModuleBidders)
        {
            bot.StartTimer(this);
        }
    }
    public void Update(BuddyBidding Module)
    {
        if (Item == Item.Buddy) return;
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = 0;
                if (!Sold)
                {
                    Sold = true;
                    Module.SellItem(this);
                }
            }
            else 
            {
                foreach(ModuleBot bot in ModuleBidders) 
                {
                    bot.Update(Module, this);
                }
            }
        }
    }
    public void OnBidPlaced()
    {
        if (Sold) return;
        Timer = 60;
        foreach (ModuleBot bot in ModuleBidders)
        {
            bot.StartTimer(this);
        }
    }
    public BidInfo GetBidInfo()
        => new BidInfo ( Item, Timer, CurrentBid, GetCostState() );
}
