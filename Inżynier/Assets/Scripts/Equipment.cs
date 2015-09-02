using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver()
	{
		if (player.equipment) {
			print ("Check Equipment");
		}
	}
}
