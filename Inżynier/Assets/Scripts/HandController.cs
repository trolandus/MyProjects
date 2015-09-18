using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public PlayerController player;
	public GameObject defaultLookAt;
	public GameObject weaponPivot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameplay) {
			//this.transform.LookAt(defaultLookAt.transform.position);
		}
	}
}
