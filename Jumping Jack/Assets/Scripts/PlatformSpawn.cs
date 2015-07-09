using UnityEngine;
using System.Collections;

public class PlatformSpawn : MonoBehaviour {

	public float startTime;
	public float repeatTime;
	public GameObject platform;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", startTime, repeatTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn()
	{
		GameObject newPlatform = Instantiate<GameObject> (platform);
		newPlatform.transform.position = this.transform.position;
	}
}
