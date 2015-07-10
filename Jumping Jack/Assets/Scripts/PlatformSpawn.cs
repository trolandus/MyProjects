using UnityEngine;
using System.Collections;

public class PlatformSpawn : MonoBehaviour {

	public float emptyProbability;
	public GameObject[] platforms;

	private float startTime = 0.0f;
	private float repeatTime = 0.55f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", startTime, repeatTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn()
	{
		float emptyPlatform = Random.Range (0.0f, 1.0f);
		if (emptyPlatform >= emptyProbability) {
			GameObject newPlatform = Instantiate<GameObject> (platforms[0]);
			newPlatform.transform.position = this.transform.position;
		} else {
			GameObject newPlatform = Instantiate<GameObject> (platforms[1]);
			newPlatform.transform.position = this.transform.position;
		}
	}

	public void Stop()
	{
		CancelInvoke ();
	}
}
