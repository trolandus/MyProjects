using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 10;
	public float turningSpeed = 30;
	public bool gameplay;
	public bool equipment;

	private float horizontal;
	private float vertical;
	private GameObject pivot;
	private GameObject body;
	private GameObject head;

	private Quaternion originalRotation;
	private float rotationX = 0;
	private float rotationY = 0;
	private float minimumX = -60f;
	private float maximumX = 60f;
	private float minimumY = -30f;
	private float maximumY = 30f;

	private Vector3 newForward;

	// Use this for initialization
	void Start () {
		gameplay = true;
		equipment = false;
		pivot = GameObject.Find ("CameraPivot");
		body = GameObject.Find ("Body");
		head = GameObject.Find ("Head");
		originalRotation = pivot.transform.localRotation;
		newForward = this.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		OpenEquipment ();
		if (gameplay) {
			Movement();
			MouseControl();
		}
		if (equipment) {
			gameplay = false;
		}
	}

	void OpenEquipment(){
		if (Input.GetKeyDown (KeyCode.I)) {
			if (equipment) {
				gameplay = true;
				equipment = false;
			} else
				equipment = true;
		}
	}

	void Movement(){
		horizontal = Input.GetAxis ("Horizontal") * movementSpeed;
		
		vertical = Input.GetAxis ("Vertical") * movementSpeed;
		body.transform.Translate (horizontal, 0, vertical);
		pivot.transform.position = head.transform.position;

		Vector3 moveDirection = new Vector3 (horizontal, 0, vertical);

		if (moveDirection != Vector3.zero) {
			body.transform.forward = newForward;
		}
	}

	void MouseControl(){
		rotationX += Input.GetAxis ("Mouse X") * turningSpeed;
		rotationY += Input.GetAxis ("Mouse Y") * turningSpeed;

		//rotationX = ClampAngle (rotationX, minimumX, maximumX);
		rotationY = ClampAngle (rotationY, minimumY, maximumY);
		
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);

		pivot.transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		newForward = new Vector3 (pivot.transform.forward.x, 0, pivot.transform.forward.z);
	}

	float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
