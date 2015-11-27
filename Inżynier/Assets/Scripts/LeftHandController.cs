using UnityEngine;
using System.Collections;

public class LeftHandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
	public Animator myAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.isComparing) {
			myAnimator.enabled = false;
			myAnimator.SetBool("Compare", false);
		}
	}
}
