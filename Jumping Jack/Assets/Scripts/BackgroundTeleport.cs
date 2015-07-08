using UnityEngine;
using System.Collections;

public class BackgroundTeleport : MonoBehaviour {

	public BackgroundTeleport output;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "Player")
			col.gameObject.transform.position = output.transform.position;
	}
}
