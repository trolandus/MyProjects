using UnityEngine;
using System.Collections;

public class Backpack : MonoBehaviour {

	public GameObject hand;

	private PlayerController player;
	private GameObject body;
	private Vector3 oldPosition;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
		body = GameObject.Find ("Body");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameplay) {
			this.transform.localPosition = new Vector3(0, 2.17f, -0.68f);
		}
		if (player.equipment) {
			this.transform.position = new Vector3(body.transform.position.x + body.transform.forward.x, 0.25f, body.transform.position.z + body.transform.forward.z);
		}
	}

	void OnMouseOver()
	{
		if (player.equipment) {
			//hand.transform.LookAt(this.transform.position);
			print("Backpack");
		}
	}
}
