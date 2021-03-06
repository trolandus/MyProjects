﻿using UnityEngine;
using System.Collections;

public class CameraController2 : MonoBehaviour {

	public PlayerController player;
	public Mixtures mixtures;
	public Scrolls scrolls;

	private GameObject pivotpoint;
	private Animator myAnimator;
	private int equipmentHash = Animator.StringToHash("Equipment");
	private int equipmentIdle = Animator.StringToHash("Base Layer.EquipmentIdle");
	private int showCloser = Animator.StringToHash ("Base Layer.ShowCloser");
	private int gameplayHash = Animator.StringToHash("Gameplay");
	private int idleHash = Animator.StringToHash("Base Layer.Idle");
	private int pickUpHash = Animator.StringToHash("PickingUp");
	private int pickUpIdle = Animator.StringToHash("Base Layer.PickingUpIdle");
	private int compareIdle = Animator.StringToHash("Base Layer.CompareIdle");

	// Use this for initialization
	void Start () {
		pivotpoint = GameObject.Find ("CameraPivot");
		myAnimator = this.GetComponent<Animator> ();
	}
	
	// LateUpdate is called once per frame after Update has finished
	void LateUpdate () {
	    if (Input.GetKeyDown(KeyCode.F1))
	    {
	        TurnOnCanvas();
	    }
		AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (GameState.Instance.currentState == States.GAMEPLAY) {
			if(stateInfo.nameHash == equipmentIdle || stateInfo.nameHash == showCloser)
			{
				myAnimator.SetBool("ShowCloser", false);
				myAnimator.ResetTrigger(equipmentHash);
				myAnimator.SetTrigger(gameplayHash);
			}
			if(stateInfo.nameHash == pickUpIdle ||
			   stateInfo.nameHash == compareIdle)
			{
				myAnimator.ResetTrigger(pickUpHash);
				myAnimator.SetTrigger(gameplayHash);
			}
			if(stateInfo.nameHash == idleHash)
			{
				myAnimator.ResetTrigger(gameplayHash);
				CameraMovement();
			}
		}
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			if(GameState.Instance.currentBackpackLayer == BackpackLayers.DRINK)
			{
				myAnimator.ResetTrigger(equipmentHash);
				myAnimator.SetTrigger(gameplayHash);
				return;
			}
			myAnimator.ResetTrigger(gameplayHash);
			myAnimator.SetTrigger(equipmentHash);
			if(player.Hand.currentObject != null && GameState.Instance.currentBackpackLayer == BackpackLayers.ITEM_CHOSEN)
			{
				myAnimator.SetBool("ShowCloser", true);
				GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
                //if(player.Hand.currentObject.GetComponent<Mixtures>())
                //    mixtures.start = true;
                //else
                //    scrolls.start = true;
			}
			if(Input.GetMouseButtonDown(1) && (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM ||
			                                                              GameState.Instance.currentBackpackLayer == BackpackLayers.ITEM_CHOSEN))
			{
				player.Hand.PutBack();
				GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
				//myAnimator.SetBool("ShowCloser", false);
				mixtures.TurnOff();
			    InputManager.Instance.rmbPressed = false;
			    //InputManager.Instance.lastKeyPressed = KeyCode.None;
			}
		}
		if (player.pickUp) {
			myAnimator.ResetTrigger(gameplayHash);
			myAnimator.SetTrigger(pickUpHash);
			if(player.isComparing)
			{
				myAnimator.SetBool("Comparing", true);
			}
			else
			{
				myAnimator.SetBool("Comparing", false);
			}
		}
	}

	void CameraMovement(){
		float offsetBack = 5;
		
		transform.rotation = (pivotpoint.transform.rotation);
		transform.position = pivotpoint.transform.position + offsetBack * -transform.forward;
	}

    public Animator GetMyAnimator()
    {
        return myAnimator;
    }

    //jako event na zakończenie animacji kamery
    public void TurnOnCanvas()
    {
        player.MindCanvas.enabled = true;
    }

    public void TurnRMBOff()
    {
        InputManager.Instance.rmbPressed = false;
    }

    public void TurnLMBOff()
    {
        InputManager.Instance.lmbPressed = false;
    }
}
