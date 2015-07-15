using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 10;
	public float turningSpeed = 60;

	private float horizontal;
	private float vertical;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxis ("Horizontal") * turningSpeed;
		transform.Rotate (0, horizontal, 0);

		vertical = Input.GetAxis ("Vertical") * movementSpeed;
		transform.Translate (0, 0, vertical);
	}
}
