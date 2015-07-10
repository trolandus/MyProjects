using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "Player") {
			this.GetComponentInChildren<UnityEngine.UI.Text> ().enabled = true;
			Destroy(GameObject.FindGameObjectWithTag("Player"));
			foreach (PlatformMovement pm in GameObject.FindObjectsOfType<PlatformMovement>())
				pm.enabled = false;
			foreach (PlatformSpawn ps in GameObject.FindObjectsOfType<PlatformSpawn>())
				ps.GetComponent<PlatformSpawn>().Stop ();
		}
	}
}
