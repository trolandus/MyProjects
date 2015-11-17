using UnityEngine;
using System.Collections;

public class BodyCollision : MonoBehaviour {

	private PlayerController player;
	private HandController hand;
	public Weapon collidingWeapon;
	public bool weaponIsActive;
	public bool weaponDetected;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
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
		if (weaponIsActive)
			PickUp (collidingWeapon);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Weapon") {
			col.GetComponent<Weapon>().isActive = true;
			weaponIsActive = true;
			collidingWeapon = col.GetComponent<Weapon>();
			col.GetComponent<Weapon>().buttonImage.transform.position = new Vector3(col.transform.position.x, col.transform.position.y + 0.5f, col.transform.position.z);
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
	}

	void PickUp(Weapon w){
		if (Input.GetKeyDown (KeyCode.E) && !player.pickUp) {
			if(player.currentWeapon !=  null)
				player.HideWeapon();
			//weapon transformation
			w.transform.position = hand.weaponPivot.transform.position;
			w.transform.rotation = hand.weaponPivot.transform.rotation;
			w.transform.Rotate(new Vector3(270, 0, 0));
			w.transform.SetParent(hand.weaponPivot.transform);
			w.WeaponOriginalRotation = w.transform.rotation;

			w.isActive = false;
			w.GetComponent<BoxCollider>().enabled = false;

			weaponIsActive = false;

			hand.myAnimator.enabled = true;
			hand.myAnimator.SetBool("Show Weapon", true);

			player.pickUp = true;
			player.pickedUpWeapon = w;
		}
	}
}
