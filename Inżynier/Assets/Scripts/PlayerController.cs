using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 10;
	public float turningSpeed = 30;
	//public bool gameplay;
	//public bool equipment;
	public bool pickUp;
	public Weapon currentWeapon;
	public Weapon pickedUpWeapon;
	public GameObject pivot;
	public GameObject body;
	public GameObject head;
	public Transform MainWeaponSlot;
	public Transform DistanceWeaponSlot;
	public Transform MinorWeaponSlot;

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
	private Weapon mainWeapon;
	public int activeWeapon;

	private bool isWielding = false;

	public bool isWalking;

	public HandController Hand {
		get { return hand;}
		set { hand = value;}
	}

	// Use this for initialization
	void Start () {
		currentWeapon = null;
		GameState.Instance.currentState = States.GAMEPLAY;
		//gameplay = true;
		//equipment = false;
		//pickUp = false;
		myAnimator = GetComponent<Animator> ();
		originalRotation = pivot.transform.localRotation;
		newForward = this.transform.forward;
		hand = GameObject.Find ("mixamorig:RightHand").GetComponent<HandController> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (!pickUp) {
//			OpenEquipment ();
//			if(currentWeapon != null)
//				TurnOffStrings();
//		}
		if (GameState.Instance.currentState == States.GAMEPLAY) {
			Movement();
			WalkAnim ();
			MouseControl();
			HideWieldWeaponAnim();
			SetActiveWeapon();
			if (!pickUp) {
				OpenEquipment ();
				if(currentWeapon != null)
					TurnOffStrings();
			}
		}
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			if(GameState.Instance.currentBackpackLayer == BackpackLayers.DRINK)
			{
				StartCoroutine("Drink");
			}
			CloseEquipment();
			//gameplay = false;
		}
		if (pickUp) {
			//gameplay = false;
			ObserveItem(pickedUpWeapon);
		}
	}

	void OpenEquipment(){
		if (Input.GetKeyDown (KeyCode.I)) {
			if(currentWeapon != null)
				HideWeapon();
			myAnimator.SetBool("Equipment", true);
			GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
			GameState.Instance.currentState = States.EQUIPMENT;
		}
	}

	void CloseEquipment(){
		if (Input.GetKeyDown (KeyCode.Escape) && GameState.Instance.currentBackpackLayer == BackpackLayers.CHOOSE_ITEM) {
			GameState.Instance.currentState = States.GAMEPLAY;
			GameState.Instance.currentBackpackLayer = BackpackLayers.NONE;
			hand.PutBack();
			hand.currentObject = null;
			hand.myAnimator.enabled = false;
			myAnimator.SetBool("Equipment", false);
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
			head.GetComponent<HeadController>().body.weaponDetected = false;
			hand.myAnimator.SetBool("Show Weapon", false);
			hand.myAnimator.enabled = false;
			GameState.Instance.currentState = States.GAMEPLAY;
			pickUp = false;
			//gameplay = true;
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

	//przełączanie broni 1,2,3
	//po przełączeniu ustawienie na current i od razu po tym WieldWeapon z rozróżnieniem animacji (switch) (ewentualnie podpiąć przejścia pod inta)

	void HideWieldWeaponAnim()
	{
		if (Input.GetKeyDown (KeyCode.F) && !isWielding) {
			myAnimator.SetBool ("WeldWeapon", true);
		} else if (Input.GetKeyDown (KeyCode.F) && isWielding) {
			myAnimator.SetBool("HideWeapon", true);
		}
	}

	public void WieldWeapon() //do odpalenia przy animacji (jako event)
	{
		myAnimator.SetBool ("WeldWeapon", false);
		if (pickedUpWeapon != null) {
			currentWeapon = pickedUpWeapon;
			pickedUpWeapon = null;
			currentWeapon.gameObject.GetComponent<Raycasting> ().enabled = false;

			isWielding = true;
		} else if (activeWeapon != 0/*MainWeaponSlot.GetComponentInChildren<Weapon> () != null*/) {
			switch(activeWeapon)
			{
			case 1:
				currentWeapon = MainWeaponSlot.GetComponentInChildren<MainWeapon> ();
				break;
			case 2:
				currentWeapon = DistanceWeaponSlot.GetComponentInChildren<DistanceWeapon>();
				break;
			}
			mainWeapon = null;

			///////////////////
//			currentWeapon.transform.position = hand.weaponPivot.transform.position;
//			currentWeapon.transform.rotation = hand.weaponPivot.transform.rotation;
//			currentWeapon.transform.Rotate(new Vector3(100, 60, 0));
//			currentWeapon.transform.SetParent(hand.weaponPivot.transform);
//			currentWeapon.WeaponOriginalRotation = currentWeapon.transform.rotation;
//			
//			currentWeapon.isActive = false;
//			currentWeapon.GetComponent<BoxCollider>().enabled = false;
			///////////////////
			//wstawić 
			currentWeapon.WieldWeapon();
			currentWeapon.WeaponOriginalRotation = currentWeapon.transform.rotation;

			isWielding = true;
		}
	}

	public void HideWeapon()
	{
		myAnimator.SetBool ("HideWeapon", false);

		Transform targetSlot = null;

		switch (currentWeapon.type) {
		case WeaponType.MAIN:
			targetSlot = MainWeaponSlot;
			break;
		case WeaponType.DISTANCE:
			targetSlot = DistanceWeaponSlot;
			break;
		case WeaponType.MINOR:
			targetSlot = MinorWeaponSlot;
			break;
		}

		mainWeapon = currentWeapon;
		currentWeapon = null;

		mainWeapon.gameObject.transform.position = targetSlot.position;
		mainWeapon.gameObject.transform.rotation = targetSlot.rotation;
		mainWeapon.gameObject.transform.SetParent (targetSlot);

		//wstawić currentWeapon.HideWeapon();

		isWielding = false;
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

	public IEnumerator Drink()
	{
		myAnimator.SetBool ("Drinking", true);
		yield return new WaitForSeconds(5.5f);
		myAnimator.SetBool ("Drinking", false);
		GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
		StopCoroutine("Drink");
	}

	void SetActiveWeapon()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1) && MainWeaponSlot.GetComponentInChildren<Weapon> () != null)
		{
			myAnimator.SetInteger("WeaponType", 1);
			myAnimator.SetBool("WeldWeapon", true);
			if(activeWeapon != 1)
			{
				if(currentWeapon != null)
					HideWeapon();
				activeWeapon = 1;
			}
			//WieldWeapon();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && DistanceWeaponSlot.GetComponentInChildren<Weapon>() != null) {
			myAnimator.SetInteger ("WeaponType", 2);
			myAnimator.SetBool("WeldWeapon", true);
			if(activeWeapon != 2)
			{
				if(currentWeapon != null)
					HideWeapon();
				activeWeapon = 2;
			}
			//WieldWeapon();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) && MinorWeaponSlot.GetComponentInChildren<Weapon> () != null) {
			myAnimator.SetInteger ("WeaponType", 3);
			myAnimator.SetBool("WeldWeapon", true);
			if(activeWeapon != 3)
			{
				if(currentWeapon != null)
					HideWeapon();
				activeWeapon = 3;
			}
			//WieldWeapon();
		}
	}

	public Animator GetMyAnimator()
	{
		return myAnimator;
	}

}
