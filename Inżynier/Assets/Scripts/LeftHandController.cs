using UnityEngine;
using System.Collections;

public class LeftHandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
    public GameObject shoulder;
	public Animator myAnimator;
	public Mixtures mixtures;
	public Scrolls scrolls;

	// Use this for initialization
	void Start ()
	{
	    myAnimator = shoulder.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameState.Instance.currentState == States.EQUIPMENT)
	    {
            //myAnimator.enabled = true;
	        myAnimator = shoulder.GetComponent<Animator>();
	    }

        if ((GameState.Instance.currentState != States.EQUIPMENT || GameState.Instance.currentBackpackLayer == BackpackLayers.CHOOSE_ITEM) && !player.isComparing)
        {
			myAnimator.enabled = false;
			myAnimator.SetBool("Compare", false);
			myAnimator.SetBool ("ChoosingMixture", false);
			myAnimator.SetBool ("ChoosingScrolls", false);
		}

		if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM)
		{
			//myAnimator.enabled = true;
            //myAnimator = shoulder.GetComponent<Animator>();
			if(player.Hand.currentObject.GetComponent<Mixtures>())
			{
				myAnimator.SetBool ("ChoosingScrolls", false);
				myAnimator.SetBool ("ChoosingMixture", true);
                if (Input.mousePosition.y < mixtures.BorderLine2(Input.mousePosition.x))
                {
                    myAnimator.SetBool("Write", true);
                }
                else
                {
                    myAnimator.SetBool("Write", false);
                }
				ChooseMixtureAnimation (mixtures.currentActiveMixtureIndex);
			}
			if(player.Hand.currentObject.GetComponent<Scrolls>())
			{
				myAnimator.SetBool ("ChoosingMixture", false);
				myAnimator.SetBool ("ChoosingScrolls", true);
			    if (Input.mousePosition.y < scrolls.BorderLine(Input.mousePosition.x))
			    {
			        myAnimator.SetBool("Write", true);
			    }
			    else
			    {
			        myAnimator.SetBool("Write", false);
			    }
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

    IEnumerator CloseAnimator()
    {
        yield return new WaitForSeconds(0.1f);
        myAnimator.enabled = false;
    }
}
