using UnityEngine;
using System.Collections;

public class LeftHandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
	public Animator myAnimator;
	public Mixtures mixtures;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.isComparing) {
			myAnimator.enabled = false;
			myAnimator.SetBool("Compare", false);
			myAnimator.SetBool ("ChoosingMixture", false);
		}

		if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM) {
			myAnimator.enabled = true;
			myAnimator.SetBool ("ChoosingMixture", true);
			ChooseMixtureAnimation (mixtures.currentActiveMixtureIndex);
		}
	}

	void ChooseMixtureAnimation(int currentMixture)
	{
		myAnimator.SetInteger ("CurrentActiveMixture", currentMixture);
	}
}
