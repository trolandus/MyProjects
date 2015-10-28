using UnityEngine;
using System.Collections;

public class AnimEvent : MonoBehaviour {

	public HandController hand;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GrabObject()
	{
		hand.currentObject.parent = hand.transform;
		hand.currentObject.localPosition = hand.weaponPivot.transform.localPosition;
		hand.myAnimator.SetBool ("Grab", false);
	}
}
