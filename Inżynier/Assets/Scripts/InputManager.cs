using System;
using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

	protected InputManager() {}

	public KeyCode lastKeyPressed = KeyCode.None;
    public bool lmbPressed = false;
    public bool rmbPressed = false;

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

    public bool PressMouseKey(int i)
    {
        bool output = false;
        if (Input.GetMouseButtonDown(i) && (!lmbPressed || !rmbPressed))
        {
            Debug.Log("MousePressed");
            switch (i)
            {
                case 0:
                    if (!lmbPressed)
                    {
                        lmbPressed = true;
                        output = true;
                    }
                    else
                    {
                        output = false;
                    }
                    break;
                case 1:
                    if (!rmbPressed)
                    {
                        rmbPressed = true;
                        output = true;
                    }
                    else
                    {
                        output = false;
                    }
                    break;
                default:
                    output = true;
                    break;
            }
        }
        else
        {
            output = false;
        }
        return output;
    }

	public IEnumerator Delay(float seconds)
	{
		yield return new WaitForSeconds (seconds);
		lastKeyPressed = KeyCode.None;
	}
}
