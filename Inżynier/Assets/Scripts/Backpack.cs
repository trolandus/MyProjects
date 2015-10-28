using UnityEngine;
using System.Collections;

public class Backpack : MonoBehaviour {

	public GameObject hand;
	public GameObject body;

	private PlayerController player;
	private Vector3 rotation;
	private Quaternion oldRotation;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
		oldRotation = this.transform.localRotation;
		rotation = new Vector3 (-90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameplay) {
			this.transform.localPosition = new Vector3(0, 0.27f, -0.11f);
			this.transform.localRotation = oldRotation;
		}
		if (player.equipment) {
			this.transform.position = new Vector3(body.transform.position.x + 0.85f*body.transform.forward.x, 0.25f, body.transform.position.z + 0.85f*body.transform.forward.z);
			this.transform.rotation = Quaternion.Euler(rotation);
		}
	}
}
