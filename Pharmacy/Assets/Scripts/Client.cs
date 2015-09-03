using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    //public List<string> orderList;
    public string trouble;
    public bool isCorrect;
    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
	    MedicineOutput.Instance.currentClient = this;
	    string s = "Good Day, Sir! I have " + trouble;
        React(s);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void React(string s)
    {
        text.text = s;
    }

    public void Positive()
    {
        React("Thank you");
        isCorrect = true;
    }

    public void Negative()
    {
        React("That didn't help!");
        isCorrect = false;
    }
}
