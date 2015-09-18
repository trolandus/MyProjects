using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {

	public GameObject defaultLookAt;
	public GameObject targetItem;
	public bool itemDetected;

	public float fieldOfViewAngle = 60.0f;
	
	// Use this for initialization
	void Start () {
		itemDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (this.transform.rotation.x);

		if (itemDetected) {
			//if (this.transform.rotation.y > 0.25f)
			FollowItem (this.targetItem);
		} else {
			SetDefault ();
		}
	}

	public void FollowItem(GameObject item)
	{
		Vector3 direction = item.transform.position - this.transform.position;
		float angle = Vector3.Angle (direction, defaultLookAt.transform.forward);

		Debug.DrawRay (this.transform.position, direction, Color.green);
		Debug.DrawRay (this.transform.position, defaultLookAt.transform.forward, Color.blue);

		if (angle < fieldOfViewAngle * 0.5f)
			this.transform.LookAt (item.transform.position);
		else
			SetDefault ();
	}

	public void SetDefault()
	{
		this.transform.LookAt (defaultLookAt.transform.position);
	}
}
