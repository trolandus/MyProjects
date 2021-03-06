﻿using UnityEngine;
using System.Collections;

public class AnimEvent : MonoBehaviour {

	public HandController hand;
    public LeftHandController leftHand; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GrabObject()
	{
		hand.currentObject.parent = hand.transform;
		if (hand.currentObject.GetComponent<Scrolls> ())
			hand.currentObject.localPosition = hand.weaponPivot.transform.localPosition + new Vector3 (0, 0.02f, 0.015f);
		else
			hand.currentObject.localPosition = hand.weaponPivot.transform.localPosition;
		hand.currentObject.localRotation = Quaternion.identity;
		hand.myAnimator.SetBool ("Grab", false);
	}

    void RunLeftFinger()
    {
        leftHand.myAnimator.enabled = true;
        if (hand.currentObject.GetComponent<Mixtures>())
            hand.mixtures.start = true; 
        else
            hand.scrolls.start = false;
    }
}
