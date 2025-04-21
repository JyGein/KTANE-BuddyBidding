using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiddingPad : MonoBehaviour
{
    public BuddyBidding Module;

    public MeshRenderer Display;
	public TextMesh Cost;

	public void SetCost(int newCost, Color newColor)
	{
		Cost.text = newCost.ToString();
		Cost.color = newColor;
    }

    public void SetDisplay(Texture newTexture)
    {
		Display.material.mainTexture = newTexture;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
