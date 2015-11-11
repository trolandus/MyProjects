using UnityEngine;
using System.Collections;

public class SingleObject : MonoBehaviour {

	public bool isActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			this.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
			//this.enabled = true;
		} else {
			this.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.white;
			this.enabled = false;
		}

//		if (GameState.Instance.currentBackpackLayer != BackpackLayers.OBSERVE_ITEM)
//			isActive = false;
	}
}
