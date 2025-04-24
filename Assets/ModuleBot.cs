using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class ModuleBot
{
    public int MaxBid;
    public float TimeTillBid = 0;
    public int CurrentBid = 0;
    public void Update(BuddyBidding Module, Auction auction)
    {
        if (auction.CurrentBid >= MaxBid) return;
        if (TimeTillBid > 0)
        {
            TimeTillBid -= Time.deltaTime;
            if(TimeTillBid <= 0) {
                TimeTillBid = 0;
                int Bid = Rnd.Range(5, 21) + auction.CurrentBid;
                if (Bid > MaxBid) Bid = MaxBid;
                CurrentBid = Bid;
                auction.OnBidPlaced();
            }
        }
    }
    public void StartTimer(Auction auction)
    {
        if (CurrentBid == auction.CurrentBid) return;
        TimeTillBid = Rnd.Range(10, 36);
    }
}
