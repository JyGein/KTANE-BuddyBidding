using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Auction
{
    public List<ModuleBot> ModuleBidders = new List<ModuleBot>();
    public Item Item;
    public int PlayerBid = 0;
    public float Timer;
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
                    return 25;
                case CostState.Module:
                    return GetMaxBotBid();
                default:
                    return 25;
            }
        }
    }
    public CostState GetCostState()
    {
        int maxBotBid = GetMaxBotBid();
        if (maxBotBid == 0 && PlayerBid == 0) return CostState.NoBid;
        if (maxBotBid > PlayerBid) return CostState.Module;
        return CostState.Player;
    }
    public int GetMaxBotBid()
    {
        int output = 0;
        foreach(ModuleBot ModBidder in ModuleBidders)
        {
            output = ModBidder.CurrentBid > output ? ModBidder.CurrentBid : output;
        }
        return output;
    }
    public void Update(BuddyBidding Module)
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            if (Timer <=0)
            {
                Timer = 0;
                //Module.SellItem(this);
            }
            else {
                foreach(ModuleBot bot in ModuleBidders) {
                    bot.Update(Module, this);
                }
            }
        }
    }
    public string GetText() 
    {
        return "";
    }
}
