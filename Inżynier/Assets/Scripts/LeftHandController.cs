using UnityEngine;
using System.Collections;

public class LeftHandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
	public Animator myAnimator;
	public Mixtures mixtures;
	public Scrolls scrolls;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.isComparing) {
			myAnimator.enabled = false;
			myAnimator.SetBool("Compare", false);
			myAnimator.SetBool ("ChoosingMixture", false);
			myAnimator.SetBool ("ChoosingScrolls", false);
		}

		if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM) {
			myAnimator.enabled = true;
			if(player.Hand.currentObject.GetComponent<Mixtures>())
			{
				myAnimator.SetBool ("ChoosingScrolls", false);
				myAnimator.SetBool ("ChoosingMixture", true);
				ChooseMixtureAnimation (mixtures.currentActiveMixtureIndex);
			}
			if(player.Hand.currentObject.GetComponent<Scrolls>())
			{
				myAnimator.SetBool ("ChoosingMixture", false);
				myAnimator.SetBool ("ChoosingScrolls", true);
				ChooseScrollAnimation (scrolls.currentActiveScrollIndex);
			}
		}
	}

	void ChooseMixtureAnimation(int currentMixture)
	{
		myAnimator.SetInteger ("CurrentActiveMixture", currentMixture);
	}

	void ChooseScrollAnimation(int currentScroll)
	{
		myAnimator.SetInteger ("CurrentActiveScroll", currentScroll);
	}
}
