using UnityEngine;
using System.Collections;

public class MinorWeapon : Weapon {

	// Use this for initialization
	void Start () {
		base.Start ();
		this.type = WeaponType.MINOR;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	public override void WieldWeapon ()
	{
		this.transform.position = hand.weaponPivot.transform.position;
		this.transform.rotation = hand.weaponPivot.transform.rotation;
		this.transform.Rotate(new Vector3(100, 60, 0));
		this.transform.SetParent(hand.weaponPivot.transform);
		
		this.isActive = false;
		this.GetComponent<BoxCollider>().enabled = false;
	}

	public override void HideWeapon()
	{
		base.HideWeapon ();
	}
}
