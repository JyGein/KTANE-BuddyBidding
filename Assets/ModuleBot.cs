using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ModuleBot
{
    public int MaxBid;
    public float TimeTillBid;
    public int CurrentBid;
    public void Update(BuddyBidding Module, Auction auction)
    {
        if (TimeTillBid > 0)
        {
            TimeTillBid -= Time.deltaTime;
            if(TimeTillBid <= 0) {
                TimeTillBid = 0;

            }
        }
    }
}
