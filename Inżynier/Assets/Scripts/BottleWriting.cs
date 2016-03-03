using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BottleWriting : MonoBehaviour {

	public InputField myInputField;
	public Text myText;

	private string note;
	private SingleObject so;

	// Use this for initialization
	void Start () {
		so = this.gameObject.GetComponent<SingleObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.Instance.currentBackpackLayer == BackpackLayers.WRITE_ON_SUBITEM && so.isActive) {
			myInputField.enabled = true;
			myInputField.textComponent.enabled = true;
			myInputField.Select();
			myInputField.ActivateInputField();
			//if (myInputField.isFocused) {
			//	myText.enabled = false;
			//}
			//note = myText.text;
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
	    GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
        myInputField.textComponent.enabled = false;
	}
}
