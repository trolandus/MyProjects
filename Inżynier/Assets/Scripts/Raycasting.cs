using UnityEngine;
using System.Collections;

public class Raycasting : MonoBehaviour {

	public GameObject target;
	public float range;

	private HeadController headController;

	// Use this for initialization
	void Start () {
		headController = target.GetComponent<HeadController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray playerDetect = new Ray (transform.position, target.transform.position - transform.position);

		Vector3 rayLength = (target.transform.position - transform.position).normalized * range;

		Debug.DrawRay (transform.position, rayLength);
		//Debug.Log (Vector3.Distance (rayLength, target.transform.forward));

		if (Physics.Raycast (playerDetect, out hit, range)) {
			if(hit.collider.tag == "Player")
			{
				headController.itemDetected = true;
				headController.targetItem = this.gameObject;
			}
		}
		else
		{
			headController.itemDetected = false;
		}
	}
}
