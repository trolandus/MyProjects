using UnityEngine;
using System.Collections;

public class BodyCollision : MonoBehaviour {

	private PlayerController player;
	private LeftHandController leftHand;
	private HandController hand;
	public Weapon collidingWeapon;
	public bool weaponIsActive;
	public bool weaponDetected;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
		leftHand = GetComponentInChildren<LeftHandController> ();
		hand = GetComponentInChildren<HandController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (collidingWeapon != null) {
			if (collidingWeapon.GetComponent<Raycasting> ().itemDetected)
				weaponDetected = true;
			else
				weaponDetected = false;
		}
		if (weaponIsActive) {
			PickUp (collidingWeapon);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Weapon") {
			col.GetComponent<Weapon>().isActive = true;
			weaponIsActive = true;
			collidingWeapon = col.GetComponent<Weapon>();
			col.GetComponent<Weapon>().buttonImage.transform.position = new Vector3(col.transform.position.x, col.transform.position.y + 0.5f, col.transform.position.z);
		}

	    if (col.tag == "Sign")
	    {
	        col.GetComponent<InteractiveSign>().isActive = true;
	    }
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Weapon") {
			col.GetComponent<Weapon>().isActive = false;
			weaponIsActive = false;
			//player.pickUp = false;
			collidingWeapon = null;
		}

        if (col.tag == "Sign")
        {
            col.GetComponent<InteractiveSign>().isActive = false;
        }
	}

	void PickUp(Weapon w){
		//if (Input.GetKeyDown (KeyCode.E) && !player.pickUp) {
        if (Input.GetMouseButtonDown(0) && !player.pickUp) {
			switch(w.type)
			{
			case WeaponType.MAIN:
				player.activeWeapon = 1;
				break;
			case WeaponType.DISTANCE:
				player.activeWeapon = 2;
				break;
			case WeaponType.MINOR:
				player.activeWeapon = 3;
				break;
			}
			player.GetMyAnimator().SetInteger("WeaponType", player.activeWeapon);
            if (player.currentWeapon != null)
            {
                player.HideWeapon();
            }
            //weapon transformation
			w.transform.position = hand.weaponPivot.transform.position;	 w.transform.SetParent(hand.weaponPivot.transform);		
			w.transform.rotation = hand.weaponPivot.transform.rotation;
			w.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
           
			w.WeaponOriginalRotation = w.transform.rotation;

			w.isActive = false;
			w.GetComponent<BoxCollider>().enabled = false;

			weaponIsActive = false;

			hand.myAnimator.enabled = true;
			hand.myAnimator.SetBool("Show Weapon", true);

            hand.myAnimator.SetBool("Show Weapon Closer", true);

		    StartCoroutine(Wait(w));
		    //player.pickUp = true;
		    //player.pickedUpWeapon = w;
		}
	}

    IEnumerator Wait(Weapon w)
    {
        yield return new WaitForSeconds(0.25f);
        GameState.Instance.currentState = States.PICKING_UP;
        player.pickUp = true;
        player.pickedUpWeapon = w;
        StopCoroutine(Wait(w));
    }
}
