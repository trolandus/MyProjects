using UnityEngine;
using System.Collections;

public class CameraController2 : MonoBehaviour {

	public PlayerController player;
	public Mixtures mixtures;

	private GameObject pivotpoint;
	private Animator myAnimator;
	private int equipmentHash = Animator.StringToHash("Equipment");
	private int equipmentIdle = Animator.StringToHash("Base Layer.EquipmentIdle");
	private int showCloser = Animator.StringToHash ("Base Layer.ShowCloser");
	private int gameplayHash = Animator.StringToHash("Gameplay");
	private int idleHash = Animator.StringToHash("Base Layer.Idle");
	private int pickUpHash = Animator.StringToHash("PickingUp");
	private int pickUpIdle = Animator.StringToHash("Base Layer.PickingUpIdle");

	// Use this for initialization
	void Start () {
		pivotpoint = GameObject.Find ("CameraPivot");
		myAnimator = this.GetComponent<Animator> ();
	}
	
	// LateUpdate is called once per frame after Update has finished
	void LateUpdate () {
		AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (GameState.Instance.currentState == States.GAMEPLAY) {
			if(stateInfo.nameHash == equipmentIdle || stateInfo.nameHash == showCloser)
			{
				myAnimator.SetBool("ShowCloser", false);
				myAnimator.ResetTrigger(equipmentHash);
				myAnimator.SetTrigger(gameplayHash);
			}
			if(stateInfo.nameHash == pickUpIdle)
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
			myAnimator.ResetTrigger(gameplayHash);
			myAnimator.SetTrigger(equipmentHash);
			if(player.Hand.currentObject != null && Input.GetKeyDown(KeyCode.Space) && GameState.Instance.currentBackpackLayer == BackpackLayers.ITEM_CHOSEN)
			{
				myAnimator.SetBool("ShowCloser", true);
				GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
				mixtures.start = true;
			}
			if(InputManager.Instance.lastKeyPressed == KeyCode.Escape && GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM)
			{
				player.Hand.PutBack();
				GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
				myAnimator.SetBool("ShowCloser", false);
				mixtures.TurnOff();
				InputManager.Instance.lastKeyPressed = KeyCode.None;
			}
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
