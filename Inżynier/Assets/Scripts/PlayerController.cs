using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 10;
	public float turningSpeed = 30;
	public bool gameplay;
	public bool equipment;
	public bool pickUp;
	public Weapon currentWeapon;
	public Weapon pickedUpWeapon;
	public GameObject pivot;
	public GameObject body;
	public GameObject head;

	private float horizontal;
	private float vertical;

	private Quaternion originalRotation;
	private float rotationX = 0;
	private float rotationY = 0;
	private float minimumX = -60f;
	private float maximumX = 60f;
	private float minimumY = -30f;
	private float maximumY = 30f;

	private Vector3 newForward;
	private Vector3 movement;
	private Animator myAnimator;
	private HandController hand;
	public bool isWalking;

	// Use this for initialization
	void Start () {
		currentWeapon = null;
		gameplay = true;
		equipment = false;
		pickUp = false;
		myAnimator = GetComponent<Animator> ();
		originalRotation = pivot.transform.localRotation;
		newForward = this.transform.forward;
		hand = GameObject.Find ("mixamorig:RightHand").GetComponent<HandController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pickUp) {
			OpenEquipment ();
			if(currentWeapon != null)
				TurnOffStrings();
		}
		if (gameplay) {
			Movement();
			WalkAnim ();
			MouseControl();
		}
		if (equipment) {
			gameplay = false;
		}
		if (pickUp) {
			gameplay = false;
			ObserveItem(pickedUpWeapon);
		}
	}

	void OpenEquipment(){
		if (Input.GetKeyDown (KeyCode.I)) {
			if (equipment) {
				gameplay = true;
				equipment = false;
				hand.PutBack();
				hand.currentObject = null;
				hand.myAnimator.enabled = false;
				myAnimator.SetBool("Equipment", false);
			} else
			{
				myAnimator.SetBool("Equipment", true);
				equipment = true;
			}
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

	public void ObserveItem(Weapon w){
		this.myAnimator.enabled = false;
		Debug.Log (CompareWeapons (w, currentWeapon));

		w.WeaponRotationX += Input.GetAxis ("Mouse X") * turningSpeed;
		w.WeaponRotationY += Input.GetAxis ("Mouse Y") * turningSpeed;

		w.WeaponRotationX = ClampAngle (w.WeaponRotationX, -30.0f, 30.0f);
		w.WeaponRotationY = ClampAngle (w.WeaponRotationY, -30.0f, 30.0f);

		Quaternion xQuaternion = Quaternion.AngleAxis (w.WeaponRotationX, -Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (w.WeaponRotationY, Vector3.forward);

		w.transform.localRotation = w.WeaponOriginalRotation * xQuaternion * yQuaternion;

		if(Input.GetKeyDown (KeyCode.F)){
			head.GetComponent<HeadController>().itemDetected = false;
			hand.myAnimator.SetBool("Show Weapon", false);
			hand.myAnimator.enabled = false;
			pickUp = false;
			gameplay = true;
			WieldWeapon();
			this.myAnimator.enabled = true;
		}
	}

	float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

//zrobić komparator dla trzech sztuk broni
	public string CompareWeapons(Weapon weapon, Weapon currentWeapon)
	{
		string s = "";
		if (currentWeapon != null) {

			if (currentWeapon.damage > weapon.damage) {
				s = weapon.name + " deals less damage than " + currentWeapon.name;
			} else if (currentWeapon.damage == weapon.damage) {
				s = weapon.name + " deals same amount of damage as " + currentWeapon.name;
			} else if (currentWeapon.damage < weapon.damage) {
				s = weapon.name + " deals more damage than " + currentWeapon.name;
			}
		} else {
			s = "You currently have " + "0" + " weapons. Take it.";
			foreach(WeaponElement e in weapon.elements)
			{
				ParticleSystem ps = e.gameObject.GetComponent<ParticleSystem>();
				ps.startColor = Color.blue;
				ps.Emit(1);
				e.statText.color = Color.blue;
				e.statText.enabled = true;
			}
		}
		return s;
	}

	void WieldWeapon()
	{
		currentWeapon = pickedUpWeapon;
		pickedUpWeapon = null;
		//currentWeapon.transform.rotation = currentWeapon.WeaponOriginalRotation;
		currentWeapon.gameObject.GetComponent<Raycasting> ().enabled = false;
	}

	void TurnOffStrings()
	{
		foreach(WeaponElement e in currentWeapon.elements)
		{
			ParticleSystem ps = e.gameObject.GetComponent<ParticleSystem>();
			e.statText.enabled = false;
		}
	}

	void WalkAnim()
	{
		if (horizontal != 0 || vertical != 0) {
			myAnimator.SetFloat("Speed", vertical);
			myAnimator.SetFloat("SpeedStrafe", horizontal);
			isWalking = true;
		} else {
			myAnimator.SetFloat("Speed", 0.0f);
			myAnimator.SetFloat("SpeedStrafe", 0.0f);
			isWalking = false;
		}
	}
}
