using UnityEngine;
using System.Collections;

public class Mixtures : MonoBehaviour {

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

	public Transform[] slots;

	public SingleObject[] mixtures;
	public bool start;

	public int currentActiveMixtureIndex = 0;
	private int i;

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
		i = 0;
		
		foreach (Transform t in slots) {
			if(t.gameObject.GetComponentInChildren<SingleObject>() != null)
			{
				mixtures[i] = t.gameObject.GetComponentInChildren<SingleObject>();
				//mixtures[currentActiveMixtureIndex].isActive = false;
				i++;
			}
		}
	}

	void ChooseMixture(int startMixtureIndex)
	{
		if (start) {
			currentActiveMixtureIndex = startMixtureIndex;
			mixtures [currentActiveMixtureIndex].enabled = true;
			mixtures [currentActiveMixtureIndex].isActive = true;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && ++currentActiveMixtureIndex < i){
			if(mixtures[currentActiveMixtureIndex] != null)
			{
				mixtures[currentActiveMixtureIndex].enabled = true;
				mixtures[currentActiveMixtureIndex].isActive = true;
			}
			DisableMixtures();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow) && --currentActiveMixtureIndex >= 0) {
			if (mixtures [currentActiveMixtureIndex] != null) {
				mixtures [currentActiveMixtureIndex].enabled = true;
				mixtures [currentActiveMixtureIndex].isActive = true;
			}
			DisableMixtures ();
		}
		if (currentActiveMixtureIndex < 0)
			currentActiveMixtureIndex = 0;
		if (currentActiveMixtureIndex >= i)
			currentActiveMixtureIndex = i - 1;

		if (Input.GetKeyDown (KeyCode.Space))
			GameState.Instance.currentBackpackLayer = BackpackLayers.WRITE_ON_SUBITEM;

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
}
