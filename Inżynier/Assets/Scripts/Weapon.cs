using UnityEngine;
using System.Collections;

public enum WeaponType {MAIN, DISTANCE, MINOR};

public class Weapon : MonoBehaviour {

	public string weaponName;
	public int damage;
	public WeaponElement[] elements;

	public UnityEngine.UI.Image buttonImage;
	public bool isActive;
	public WeaponType type;

	private Quaternion weaponOriginalRotation;
	private float weaponRotationX = 0;
	private float weaponRotationY = 0;

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
	void Start () {
		buttonImage.enabled = false;
		isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			buttonImage.enabled = true;
		}
		else
			buttonImage.enabled = false;
	}
}
