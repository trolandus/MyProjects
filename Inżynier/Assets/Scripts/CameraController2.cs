using UnityEngine;
using System.Collections;

public class CameraController2 : MonoBehaviour {

	public PlayerController player;

	private GameObject pivotpoint;
	private Animator myAnimator;
	private int equipmentHash = Animator.StringToHash("Equipment");
	private int equipmentIdle = Animator.StringToHash("Base Layer.EquipmentIdle");
	private int gameplayHash = Animator.StringToHash("Gameplay");
	private int idleHash = Animator.StringToHash("Base Layer.Idle");
	private int pickUpHash = Animator.StringToHash("PickingUp");

	// Use this for initialization
	void Start () {
		pivotpoint = GameObject.Find ("CameraPivot");
		myAnimator = this.GetComponent<Animator> ();
	}
	
	// LateUpdate is called once per frame after Update has finished
	void LateUpdate () {
		AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (player.gameplay) {
			if(stateInfo.nameHash == equipmentIdle)
			{
				myAnimator.ResetTrigger(equipmentHash);
				myAnimator.SetTrigger(gameplayHash);
			}
			if(stateInfo.nameHash == idleHash)
			{
				myAnimator.ResetTrigger(gameplayHash);
				CameraMovement();
			}
		}
		if (player.equipment) {
			myAnimator.ResetTrigger(gameplayHash);
			myAnimator.SetTrigger(equipmentHash);
		}
		if (player.pickUp) {
			myAnimator.ResetTrigger(gameplayHash);
			myAnimator.SetTrigger(pickUpHash);
		}
	}

	void CameraMovement(){
		float offsetBack = 5;
		
		transform.rotation = (pivotpoint.transform.rotation);
		transform.position = pivotpoint.transform.position + offsetBack * -transform.forward;
	}
}
