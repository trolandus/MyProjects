using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

	protected InputManager() {}

	public KeyCode lastKeyPressed = KeyCode.None;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lastKeyPressed != KeyCode.Escape) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				lastKeyPressed = KeyCode.Escape;
			}
		}
	}

	public IEnumerator Delay(float seconds)
	{
		yield return new WaitForSeconds (seconds);
		lastKeyPressed = KeyCode.None;
	}
}
