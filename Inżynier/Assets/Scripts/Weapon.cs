using UnityEngine;
using System.Collections;

public enum WeaponType {MAIN, DISTANCE, MINOR};

public class Weapon : MonoBehaviour {

	public string weaponName;
	public int damage;
	public WeaponElement[] elements;
	public HandController hand;
	public LeftHandController leftHand;

	public UnityEngine.UI.Image buttonImage;
	public bool isActive;
	public WeaponType type;

	protected Quaternion weaponOriginalRotation;
	protected float weaponRotationX = 0;
	protected float weaponRotationY = 0;

	public Quaternion WeaponOriginalRotation {
		set {
			weaponOriginalRotation = value;
		}
		get {
			return weaponOriginalRotation;
		}
	}

	public float WeaponRotationX {
		set {
			weaponRotationX = value;
		}
		get {
			return weaponRotationX;
		}
	}

	public float WeaponRotationY {
		set {
			weaponRotationY = value;
		}
		get {
			return weaponRotationY;
		}
	}

	// Use this for initialization
	public void Start () {
		buttonImage.enabled = false;
		isActive = false;

		foreach (WeaponElement e in this.elements) {
			ParticleSystem ps = e.gameObject.GetComponent<ParticleSystem> ();
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if (isActive) {
			buttonImage.enabled = true;
		}
		else
			buttonImage.enabled = false;
	}

	public virtual void WieldWeapon()
	{
		return;
	}

	public virtual void HideWeapon()
	{
		return;
	}

	public string Compare(ref bool isComparing, Weapon playerWeapon)
	{
		string output;
		Color col;
		Color col2 = Color.blue;
		int textToChange = -1;

		if (playerWeapon != null) {
			leftHand.myAnimator.enabled = true;
			leftHand.myAnimator.SetBool("Compare", true);

			playerWeapon.transform.position = leftHand.weaponPivot.transform.position;
			playerWeapon.transform.rotation = leftHand.weaponPivot.transform.rotation;

			if(this.damage > playerWeapon.damage)
			{
				col = Color.green;
				col2 = Color.red;
				textToChange = 0;
			}
			else if(this.damage == playerWeapon.damage)
			{
				col = Color.blue;
				col2 = Color.blue;
				textToChange = 0;
			}
			else
			{
				col = Color.red;
				col2 = Color.green;
				textToChange = 0;
			}

			isComparing = true;
			output = "Comparing weapons";
		} else {
			col = Color.blue;
			output = "";
		}

		TurnOnParticles (this.elements, col, textToChange);

		if (playerWeapon != null)
			TurnOnParticles (playerWeapon.elements, col2, textToChange);

		return output;
	}

	void TurnOnParticles(WeaponElement[] array, Color col, int textToChange)
	{
		foreach (WeaponElement e in array) {
			ParticleSystem ps = e.gameObject.GetComponent<ParticleSystem>();
			
			if(textToChange != -1)
			{
				e.SetColor(textToChange, col);
			}
			else
			{
				ps.startColor = col;
				e.statText.color = col;
				e.statText.enabled = true;
			}
			
			ps.Emit(1);
		}
	}

}
