﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCard : MonoBehaviour {

    public long CreditCardNumber;
    public int CVVNumber;
    public Company Company;
    public string CreditCardNumberStr {
        get {
            return Util.LongToText(CreditCardNumber, 16);
        }
    }

    public BuddyBidding Module;

    public TextMesh CCN;
    public TextMesh CVV;
    public List<Material> CompanyMats;

    public delegate void CCLog(string message);
    public CCLog Log;
	bool Flip ()
	{
		transform.Rotate(new Vector3(180, 0));
        Module.Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, gameObject.transform);
        return false;
    }

	// Use this for initialization
	void Start () {

    }

    void Awake()
    {
        transform.GetComponent<KMSelectable>().OnInteract += Flip;
        CreditCardNumber = (long)UnityEngine.Random.Range((long)0, 10000000000000000);
        CVVNumber = (int)UnityEngine.Random.Range(100, 401);
        Company = (Company)UnityEngine.Random.Range(0, 3);
        CCN.text = Util.LongToText(CreditCardNumber, 16).Insert(12, " ").Insert(8, " ").Insert(4, " ");
        CVV.text = CVVNumber.ToString();
        GetComponent<MeshRenderer>().material = CompanyMats[(int)Company];
        Module.Log($"Credit Card Number: {CreditCardNumber}");
        Module.Log($"CVV: {CVVNumber}");
        Module.Log($"Company: {Company}");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
