using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public HandController hand;
	
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
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			//hand.transform.LookAt(this.transform.position);
			print ("Check Equipment");
		}
	}
}
