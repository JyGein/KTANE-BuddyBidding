using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class BiddingPad : MonoBehaviour
{
    public BuddyBidding Module;

    public MeshRenderer Display;
	public TextMesh Cost;
    public TextMesh Timer;
    public MeshRenderer Sold;
    public float TimeLeft {
        set {
            int i = (int)Math.Ceiling(value);
            int minutes = (i / 60);
            string text = minutes.ToString();
            text += ":";
            text += Util.IntToText(i % 60, 2);
            Timer.text = text;
        }
    }

	public void SetCost(string newCost, Color newColor)
	{
		Cost.text = newCost;
		Cost.color = newColor;
    }

    public void SetDisplay(Texture newTexture)
    {
		Display.material.mainTexture = newTexture;
    }

    public void UpdateDisplay(BidInfo bidInfo, bool PlayerInput = false) {
        if (bidInfo.item == Item.Buddy)
        {
            SetDisplay(Module.Buddies[(int)Module.TodaysBuddy]);
            Timer.text = "";
            Sold.enabled = false;
        }
        else
        {
            SetDisplay(Module.Items[(int)bidInfo.item]);
            TimeLeft = bidInfo.timeLeft;
            Sold.enabled = bidInfo.timeLeft == 0;
        }
        SetCost((bidInfo.costState == CostState.Default && !PlayerInput) ? "$$$" : Util.IntToText(bidInfo.currentBid, 3), Util.CostColors[bidInfo.costState]);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
