using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;
	public Transform currentObject;
	public Animator myAnimator;

	public Mixtures mixtures;
	public Scrolls scrolls;

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
		if (GameState.Instance.currentState == States.GAMEPLAY) {
			//this.transform.LookAt(defaultLookAt.transform.position);
			//myAnimator.enabled = false;
		}
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			myAnimator.enabled = true;
			if(GameState.Instance.currentBackpackLayer != BackpackLayers.ITEM_CHOSEN)
				ManageEquipment ();
			if(GameState.Instance.currentBackpackLayer == BackpackLayers.DRINK)
			{
				myAnimator.enabled = false;
			}
		}

	    if (GameState.Instance.currentState == States.PICKING_UP)
	    {
	        myAnimator.SetBool("PointAt", false);
            myAnimator.SetBool("PutBack", false);
	    }
	}

	void LateUpdate(){
		if (GameState.Instance.currentBackpackLayer == BackpackLayers.CHOOSE_ITEM) {
			MoveHand(currentObject);
		}
	}

	void ManageEquipment(){

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.tag == "BackpackItem") {
				currentObject = hit.collider.transform;
				//currentObject.localRotation = Quaternion.Euler(new Vector3(351.0f, 346.0f, 0.0f));
			}
		} else
			if(!isGrabing)
				currentObject = null;
	}

	void MoveHand(Transform obj){
		if (obj != null) {
			myAnimator.SetBool ("PointAt", true);
			if(Input.GetMouseButton(0))
			{
				if(currentObject.GetComponent<Mixtures>())
					scrolls.enabled = false;
				else
					mixtures.enabled = false;
				GrabItem();
			}
		}
		else
			if(!isGrabing)
				myAnimator.SetBool ("PointAt", false);
	}

	void GrabItem(){
		myAnimator.SetBool ("PutBack", false);
		isGrabing = true;
		GameState.Instance.currentBackpackLayer = BackpackLayers.ITEM_CHOSEN;
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
				currentObject.transform.localRotation = currentObject.GetComponent<Mixtures> ().OldRotation;
				currentObject.transform.localScale = currentObject.GetComponent<Mixtures>().OldScale;
				scrolls.enabled = true;
			}
			if(currentObject.GetComponent<Scrolls>()){
				currentObject.transform.parent = currentObject.GetComponent<Scrolls> ().OldParent;
				currentObject.transform.localPosition = currentObject.GetComponent<Scrolls> ().OldPosition;
				currentObject.transform.localRotation = currentObject.GetComponent<Scrolls> ().OldRotation;
				currentObject.transform.localScale = currentObject.GetComponent<Scrolls>().OldScale;
				mixtures.enabled = true;
			}
		}
	}
}
