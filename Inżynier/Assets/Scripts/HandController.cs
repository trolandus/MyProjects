using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
	public Animator myAnimator;

	private Ray ray;
	private RaycastHit hit;
	private Vector3 oldPosition;
	private bool isGrabing;

	private float yawMouse;
	private float pitchMouse;


	// Use this for initialization
	void Start () {
		oldPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameplay) {
			//this.transform.LookAt(defaultLookAt.transform.position);
			//myAnimator.enabled = false;
		}
		if (player.equipment) {
			myAnimator.enabled = true;
			ManageEquipment ();
		}
	}

	void LateUpdate(){
		if (player.equipment) {
			MoveHand(currentObject);
		}
	}

	void ManageEquipment(){

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.tag == "BackpackItem") {
				currentObject = hit.collider.transform;
			}
		} else
			if(!isGrabing)
				currentObject = null;
	}

	void MoveHand(Transform obj){
		if (obj != null) {
			myAnimator.SetBool ("PointAt", true);
			if(Input.GetMouseButton(0))
				GrabItem();
		}
		else
			if(!isGrabing)
				myAnimator.SetBool ("PointAt", false);
	}

	void GrabItem(){
		myAnimator.SetBool ("PutBack", false);
		isGrabing = true;
		myAnimator.SetBool ("Grab", true);
	}

	public void PutBack(){
		if (currentObject != null) {
			myAnimator.SetBool ("PutBack", true);
			currentObject.parent = null;
			isGrabing = false;
			if (currentObject.GetComponent<Mixtures> ()) {
				currentObject.transform.parent = currentObject.GetComponent<Mixtures> ().OldParent;
				currentObject.transform.localPosition = currentObject.GetComponent<Mixtures> ().OldPosition;
			}
		}
	}
}
