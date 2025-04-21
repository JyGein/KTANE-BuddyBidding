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
    public float TimeLeft {
        set {
            int i = (int)Math.Round(value, MidpointRounding.AwayFromZero);
            int minutes = (i % 60);
            string text = minutes.ToString();
            text += ":";
            text += (i - (minutes * 60)).ToString();
            Timer.text = text;
        }
    }

	public void SetCost(int newCost, Color newColor)
	{
		Cost.text = newCost.ToString();
		Cost.color = newColor;
    }

    public void SetDisplay(Texture newTexture)
    {
		Display.material.mainTexture = newTexture;
    }

    public void UpdateDisplay(BidInfo bidInfo) {
        SetDisplay(Module.Items[(int)bidInfo.item]);
        SetCost(bidInfo.currentBid, Util.CostColors[bidInfo.costState]);
        TimeLeft = bidInfo.timeLeft;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
