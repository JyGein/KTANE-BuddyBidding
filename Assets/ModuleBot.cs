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
    public delegate void PlaceBidAction(int amount);
    public void Update()
    {
        if (TimeTillBid > 0)
        {
            TimeTillBid -= Time.deltaTime;
        }
    }
}
