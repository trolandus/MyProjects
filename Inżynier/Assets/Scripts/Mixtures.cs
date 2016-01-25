using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Mixtures : MonoBehaviour
{
	private Vector3 oldPosition;
	private Quaternion oldRotation;
	private Vector3 oldScale;
	private Transform oldParent;

	public Vector3 OldPosition
	{
		get {return oldPosition;}
		set {oldPosition = value;}
	}

	public Transform OldParent {
		get { return oldParent;}
		set { oldParent = value;}
	}

	public Quaternion OldRotation {
		get { return oldRotation;}
		set { oldRotation = value;}
	}

	public Vector3 OldScale {
		get { return oldScale;}
		set { oldScale = value;}
	}

	public Transform leftHand;
	public Transform[] slots;

	public SingleObject[] mixtures;
	public bool start;

	public int currentActiveMixtureIndex = 0;
	public int listIndex;
	private List<int> mixturesIndices = new List<int>();
    public double border;

	// Use this for initialization
	void Start () {
		oldParent = this.transform.parent;
		oldPosition = this.transform.localPosition;
		oldRotation = this.transform.localRotation;
		oldScale = this.transform.localScale;
		mixtures = new SingleObject[slots.Length];

		UpdateMixturesCount ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM) {
			ChooseMixture(0);
			DrinkMixture();
		} else
			UpdateMixturesCount ();

		if (InputManager.Instance.lastKeyPressed == KeyCode.Escape && GameState.Instance.currentBackpackLayer == BackpackLayers.WRITE_ON_SUBITEM) {
			TurnOff();
			GameState.Instance.currentBackpackLayer = BackpackLayers.OBSERVE_ITEM;
			InputManager.Instance.lastKeyPressed = KeyCode.None;
			//StartCoroutine (InputManager.Instance.Delay (1.0f));
		}
	}

	void UpdateMixturesCount()
	{
		int i = 0;
		mixturesIndices.Clear ();
		
		foreach (Transform t in slots) {
			if(t.gameObject.GetComponentInChildren<SingleObject>() != null)
			{
				mixtures[i] = t.gameObject.GetComponentInChildren<SingleObject>();
				mixturesIndices.Add(i);
				//mixtures[currentActiveMixtureIndex].isActive = false;
			}
			i++;
		}

		if (mixturesIndices.Count == 0)
			currentActiveMixtureIndex = -1;
	}

    float BorderLine(float x)
    {
        return 0.98f*x - 250.36f;
        //return 1.06f*x - 398.36f;
    }

    public float BorderLine2(float x)
    {
        return -x + 953;
    }

	void ChooseMixture(int startMixtureIndex)
	{
        //Debug.Log(Input.mousePosition.y + " " + BorderLine2(Input.mousePosition.x));
        Debug.Log(Input.mousePosition);
		if (start) {
			currentActiveMixtureIndex = startMixtureIndex;
			listIndex = 0;
			mixtures [currentActiveMixtureIndex].enabled = true;
			mixtures [currentActiveMixtureIndex].isActive = true;
		}
        //if (Input.mousePosition.x < Screen.width * 0.6084f)
        if(Input.mousePosition.y > BorderLine(Input.mousePosition.x))
        {
            if (++listIndex >= mixturesIndices.Count)
                listIndex = mixturesIndices.Count - 1;
            currentActiveMixtureIndex = mixturesIndices[listIndex];
            if (mixtures[currentActiveMixtureIndex] != null)
            {
                mixtures[currentActiveMixtureIndex].enabled = true;
                mixtures[currentActiveMixtureIndex].isActive = true;
            }
            DisableMixtures();
        }
        else
        {
            if (--listIndex < 0)
                listIndex = 0;
            currentActiveMixtureIndex = mixturesIndices[listIndex];
            if (mixtures[currentActiveMixtureIndex] != null)
            {
                mixtures[currentActiveMixtureIndex].enabled = true;
                mixtures[currentActiveMixtureIndex].isActive = true;
            }
            DisableMixtures();
        }

	    border = Screen.height*0.482 + 51*currentActiveMixtureIndex;

		//if (currentActiveMixtureIndex < 0)
		//	currentActiveMixtureIndex = 0;
		//if (currentActiveMixtureIndex >= i)
		//	currentActiveMixtureIndex = i - 1;

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < BorderLine2(Input.mousePosition.x) && !start)
	    {
	        GameState.Instance.currentBackpackLayer = BackpackLayers.WRITE_ON_SUBITEM;
	        mixtures[currentActiveMixtureIndex].GetComponent<BottleWriting>().myText.text = "";
	    }

	    start = false;
	}

	void DisableMixtures()
	{
		for (int j = 0; j < mixtures.Length; j++) {
			if(j != currentActiveMixtureIndex)
			{
				if(mixtures[j] != null)
					mixtures[j].isActive = false;
			}
		}
	}

	public void TurnOff()
	{
		foreach (SingleObject so in mixtures) {
			if(so != null)
				so.isActive = false;
		}
	}

	void DrinkMixture()
	{
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y >= BorderLine2(Input.mousePosition.x) && !start)
		{
			GameObject m = mixtures[currentActiveMixtureIndex].gameObject;
			m.transform.parent = leftHand;
			m.transform.localPosition = new Vector3(0.0125f, 0.0857f, 0.0564f);
			mixtures[currentActiveMixtureIndex] = null;
			Destroy(mixtures[currentActiveMixtureIndex]);
			mixturesIndices.Remove(currentActiveMixtureIndex);
			if(mixturesIndices.Count != 0)
				currentActiveMixtureIndex = mixturesIndices[0];
			GameState.Instance.currentBackpackLayer = BackpackLayers.DRINK;
			Destroy(m);
			UpdateMixturesCount();
		}
	}
}
