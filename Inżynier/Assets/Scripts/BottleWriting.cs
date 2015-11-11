using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottleWriting : MonoBehaviour {

	public InputField myInputField;
	public Text myText;

	private string note;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.Instance.currentState == States.EQUIPMENT) {
			myInputField.enabled = true;
			if (myInputField.isFocused) {
				myText.enabled = false;
			}
			note = myText.text;
		} else
			myInputField.enabled = false;
	}

	public void OnValueChange(string s){

	}

	public void OnEndEdit(string s){
		Debug.Log ("koniec");
		//myText.text = s;
		myText.enabled = true;
		myText.text = s;
		myInputField.text = " ";
	}
}
