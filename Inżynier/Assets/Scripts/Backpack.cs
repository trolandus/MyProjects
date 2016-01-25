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
		GameState.Instance.currentBackpackLayer = BackpackLayers.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.Instance.currentState == States.GAMEPLAY) {
            this.transform.parent = body.transform;
			this.transform.localPosition = new Vector3(0, 1.27f, -0.11f);
			this.transform.localRotation = oldRotation;
		}
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			this.transform.position = new Vector3(body.transform.position.x + 0.8f*body.transform.forward.x, 0.25f, body.transform.position.z + 0.8f*body.transform.forward.z);
		    this.transform.parent = null;
		    //this.transform.rotation = Quaternion.Euler(rotation);
		}
	}
}
