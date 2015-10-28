using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;

	private Ray ray;
	private RaycastHit hit;
	private Vector3 oldPosition;

	// Use this for initialization
	void Start () {
		oldPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameplay) {
			//this.transform.LookAt(defaultLookAt.transform.position);
		}
		if (player.equipment) {
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
				Debug.Log ("BackpackItem");
			}
		} else
			currentObject = null;
	}

	void MoveHand(Transform obj){
		if (obj != null)
			this.transform.position = obj.position;
		else
			this.transform.localPosition = oldPosition;
	}

	public Vector3 GetOldPosition(){
		return oldPosition;
	}
}
