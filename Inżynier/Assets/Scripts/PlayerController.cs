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
    public Weapon leftHandWeapon;
	public GameObject pivot;
	public GameObject body;
	public GameObject head;
	public Transform MainWeaponSlot;
	public Transform DistanceWeaponSlot;
	public Transform MinorWeaponSlot;
    public Canvas MindCanvas;

	public bool isComparing = false;

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
    private Vector3 oldDefaultLookAtPosition;
	private Animator myAnimator;
	private HandController hand;
    private LeftHandController leftHand;
	private Weapon mainWeapon;
    public Weapon weaponToTake;
    public Weapon weaponToDrop;
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
	    leftHand = GameObject.Find("mixamorig:LeftHand").GetComponent<LeftHandController>();
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
					TurnOffStrings(currentWeapon);
			}
		}
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			if(GameState.Instance.currentBackpackLayer == BackpackLayers.DRINK)
			{
				StartCoroutine("Drink");
			}
            
	        if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM)
	        {
	            if ((hand.currentObject.GetComponent<Mixtures>() != null && hand.mixtures.currentActiveMixtureIndex == -1) ||
                    (hand.currentObject.GetComponent<Scrolls>() != null && hand.scrolls.scrollsCount == 0))
	            {
                    //GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
	                leftHand.myAnimator.SetBool("ChoosingScrolls", false);
                    leftHand.myAnimator.SetBool("ChoosingMixture", false);
                    leftHand.myAnimator.enabled = false;
	                hand.PutBack();
	                StartCoroutine("WaitToChangeState");
	            }
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
		//if (Input.GetKeyDown (KeyCode.I)) {
        if (InputManager.Instance.PressMouseKey(1)) {
			if(currentWeapon != null)
				HideWeapon();
			myAnimator.SetBool("Equipment", true);
            Camera.main.GetComponent<CameraController2>().GetMyAnimator().SetBool("ShowCloser", true);
			GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
            GameState.Instance.currentState = States.EQUIPMENT;
		}
	}

	void CloseEquipment(){
		//if (Input.GetKeyDown (KeyCode.Escape) && GameState.Instance.currentBackpackLayer == BackpackLayers.CHOOSE_ITEM) {
        if (InputManager.Instance.PressMouseKey(1) && GameState.Instance.currentBackpackLayer == BackpackLayers.CHOOSE_ITEM) {
			GameState.Instance.currentState = States.GAMEPLAY;
			GameState.Instance.currentBackpackLayer = BackpackLayers.NONE;
			hand.PutBack();
			hand.currentObject = null;
            hand.myAnimator.SetBool("PointAt", false);
            hand.myAnimator.SetBool("PointAtScrolls", false);
            hand.myAnimator.SetBool("PointAtMixtures", false);
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
		Debug.Log (CompareWeapons (w));

		w.WeaponRotationX += Input.GetAxis ("Mouse X") * turningSpeed;
		w.WeaponRotationY += Input.GetAxis ("Mouse Y") * turningSpeed;

		w.WeaponRotationX = ClampAngle (w.WeaponRotationX, -30.0f, 30.0f);
		w.WeaponRotationY = ClampAngle (w.WeaponRotationY, -30.0f, 30.0f);

		Quaternion xQuaternion = Quaternion.AngleAxis (w.WeaponRotationX, -Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (w.WeaponRotationY, Vector3.forward);

        Debug.Log(xQuaternion + " " + yQuaternion);

	    if (isComparing)
	    {
	        xQuaternion = new Quaternion(1.0f, 0.0f, 0.0f, 1.0f);
	        yQuaternion = new Quaternion(-1.0f, 0.0f, 0.0f, 1.0f);
	    }
	    else
	    {
	        xQuaternion = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
	        yQuaternion = new Quaternion(-1.0f, 0.25f, 0.0f, 1.0f);
	    }
	    w.transform.localRotation = w.WeaponOriginalRotation * xQuaternion * yQuaternion;

	    if (!isComparing)
	    {
            //hand.myAnimator.SetBool("Show Weapon Closer", false);
	        weaponToTake = w;
	    }
	    else
	    {
            //foreach (WeaponElement we in leftHandWeapon.elements)
            //{
            //    we.isLeft = true;
            //    we.statText.transform.position = we.GetParticleSystem().transform.position;
            //}
	        weaponToTake = w;
            weaponToDrop = leftHandWeapon; 
	    }

	    if (/*Input.GetKeyDown(KeyCode.LeftArrow)*/Input.mousePosition.x <= Screen.width / 2 && isComparing)
	    {
	        head.GetComponent<HeadController>().myAnimator.enabled = true;
            head.GetComponent<HeadController>().myAnimator.SetBool("Turn Left", true);
	        hand.myAnimator.SetBool("Show Weapon Closer", false);
            leftHand.myAnimator.SetBool("Compare Closer", true);
	        weaponToTake = leftHandWeapon;
	        weaponToDrop = w;
	    }

        if (/*Input.GetKeyDown(KeyCode.RightArrow)*/Input.mousePosition.x > Screen.width / 2 && isComparing)
	    {
	        head.GetComponent<HeadController>().myAnimator.enabled = true;
            head.GetComponent<HeadController>().myAnimator.SetBool("Turn Left", false);
            hand.myAnimator.SetBool("Show Weapon Closer", true);
            leftHand.myAnimator.SetBool("Compare Closer", false);
	        weaponToDrop = leftHandWeapon;
	        weaponToTake = w;
	    }

		if(/*Input.GetKeyDown (KeyCode.E)*/InputManager.Instance.PressMouseKey(0)){
			isComparing = false;
			head.GetComponent<HeadController>().body.weaponDetected = false;
		    head.GetComponent<HeadController>().myAnimator.enabled = false;
			hand.myAnimator.SetBool("Show Weapon", false);
            leftHand.myAnimator.SetBool("Compare Closer", false);
			hand.myAnimator.enabled = false;
			GameState.Instance.currentState = States.GAMEPLAY;
			pickUp = false;
			//gameplay = true;
            //switch(w.type)
            //{
            //case WeaponType.MAIN:
            //    DropComparedWeapon(MainWeaponSlot.GetComponentInChildren<Weapon>());
            //    break;
            //case WeaponType.DISTANCE:
            //    DropComparedWeapon(DistanceWeaponSlot.GetComponentInChildren<Weapon>());
            //    break;
            //case WeaponType.MINOR:
            //    DropComparedWeapon(MinorWeaponSlot.GetComponentInChildren<Weapon>());
            //    break;
            //}
            DropComparedWeapon(weaponToDrop);
            weaponToTake.gameObject.transform.SetParent(hand.weaponPivot.transform);
		    weaponToTake.transform.position = hand.weaponPivot.transform.position;
			WieldWeapon(weaponToTake);
		    weaponToDrop = null;
		    weaponToTake = null;
		    if (isComparing)
		    {
		        foreach (WeaponElement we in leftHandWeapon.elements)
		        {
		            we.isLeft = false;
		        }
		    }
		    leftHandWeapon = null;
			this.myAnimator.enabled = true;
		}

		if (/*Input.GetKeyDown (KeyCode.Escape)*/InputManager.Instance.PressMouseKey(1)) {
            head.GetComponent<HeadController>().body.weaponDetected = false;
            head.GetComponent<HeadController>().myAnimator.enabled = false;
			hand.myAnimator.SetBool("Show Weapon", false);
			hand.myAnimator.enabled = false;
            if (isComparing)
            {
                foreach (WeaponElement we in leftHandWeapon.elements)
                {
                    we.isLeft = false;
                }
            }
			GameState.Instance.currentState = States.GAMEPLAY;
			pickUp = false;

			TurnOffStrings(w);
			DropComparedWeapon(w);
			pickedUpWeapon = null;

			HideWeapon(MainWeaponSlot.GetComponentInChildren<Weapon>());
			HideWeapon(DistanceWeaponSlot.GetComponentInChildren<Weapon>());
			HideWeapon(MinorWeaponSlot.GetComponentInChildren<Weapon>());

			this.myAnimator.enabled = true;
			isComparing = false;
		    InputManager.Instance.rmbPressed = false;
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
	public string CompareWeapons(Weapon weapon)
	{
		string s = "";

		switch (weapon.type) {
		case WeaponType.MAIN:
		    leftHandWeapon = MainWeaponSlot.GetComponentInChildren<Weapon>();
			s = weapon.Compare(ref isComparing, leftHandWeapon);
			break;
		case WeaponType.DISTANCE:
		    leftHandWeapon = DistanceWeaponSlot.GetComponentInChildren<Weapon>();
			s = weapon.Compare(ref isComparing, leftHandWeapon);
			break;
		case WeaponType.MINOR:
		    leftHandWeapon = MinorWeaponSlot.GetComponentInChildren<Weapon>();
			s = weapon.Compare(ref isComparing, leftHandWeapon);
			break;
		}

		return s;
	}

	void HideWieldWeaponAnim()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !isWielding) {
			myAnimator.SetBool ("WeldWeapon", true);
		} else if (Input.GetKeyDown (KeyCode.Space) && isWielding) {
			myAnimator.SetBool("HideWeapon", true);
		}
	}

	public void DropComparedWeapon(Weapon weapon)
	{
		if (weapon != null) {
			weapon.transform.SetParent (null);
			weapon.transform.position = body.transform.position;
			weapon.transform.rotation = Quaternion.Euler (0, 0, 270);

			weapon.isActive = true;
			weapon.GetComponent<Raycasting>().enabled = true;
			weapon.GetComponent<BoxCollider>().enabled = true;

			//pickedUpWeapon = null;
			TurnOffStrings(weapon);
		}
	}

	public void WieldWeapon(Weapon w) //do odpalenia przy animacji (jako event)
	{
		myAnimator.SetBool ("WeldWeapon", false);
		if (w != null) {
			currentWeapon = w;
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
			case 3:
				currentWeapon = MinorWeaponSlot.GetComponentInChildren<MinorWeapon>();
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

	void HideWeapon(Weapon targetWeapon)
	{
		if (targetWeapon != null) {
			Transform targetSlot = null;
		
			switch (targetWeapon.type) {
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

			targetWeapon.gameObject.transform.position = targetSlot.position;
			targetWeapon.gameObject.transform.rotation = targetSlot.rotation;
			targetWeapon.gameObject.transform.SetParent (targetSlot);

			TurnOffStrings(targetWeapon);
		}

	}

	void TurnOffStrings(Weapon weapon)
	{
		foreach(WeaponElement e in weapon.elements)
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
        leftHand.myAnimator.SetBool("Drinking", true);
	    leftHand.myAnimator.enabled = false;
		yield return new WaitForSeconds(4.5f);
		myAnimator.SetBool ("Drinking", false);
	    leftHand.myAnimator.enabled = true;
        leftHand.myAnimator.SetBool ("Drinking", false);
		GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
		StopCoroutine("Drink");
	}

    public IEnumerator WaitToChangeState()
    {
        yield return new WaitForSeconds(1f);
        GameState.Instance.currentBackpackLayer = BackpackLayers.CHOOSE_ITEM;
        StopCoroutine("WaitToChangeState");
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
