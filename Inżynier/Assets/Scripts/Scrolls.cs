using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scrolls : MonoBehaviour {

	private Vector3 oldPosition;
	private Quaternion oldRotation;
	private Vector3 oldScale;
	private Transform oldParent;
	private Animator myAnimator;

	public int currentActiveScrollIndex;
	public int scrollsCount;
	public GameObject[] scrolls;
	public bool start;

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

	// Use this for initialization
	void Start () {
		oldParent = this.transform.parent;
		oldPosition = this.transform.localPosition;
		oldRotation = this.transform.localRotation;
		oldScale = this.transform.localScale;
		myAnimator = GetComponent<Animator> ();
		myAnimator.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.Instance.currentBackpackLayer == BackpackLayers.OBSERVE_ITEM) {
			if(scrollsCount != 0)
			{
				ChooseScroll (SetCurrentIndex());
				UseScroll (currentActiveScrollIndex);
			}
		} else {
			UpdateScrollsCount ();
			myAnimator.enabled = false;
		}
	}
	void UpdateScrollsCount()
	{
		scrollsCount = 0;

		foreach (GameObject go in scrolls) {
			if(go.activeInHierarchy)
				scrollsCount++;
		}
	}

	void ChooseScroll(int startIndex)
	{
		if (start) {
			currentActiveScrollIndex = startIndex;
			if(scrollsCount != 0)
				myAnimator.enabled = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			NextAvailableScroll();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			PreviousAvailableScroll();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			scrolls[currentActiveScrollIndex].GetComponentInChildren<InputField>().ActivateInputField();
		}

		myAnimator.SetInteger ("CurrentActiveScroll", currentActiveScrollIndex);

		start = false;
	}

	void UseScroll(int index)
	{
		if (Input.GetKeyDown (KeyCode.E)) {
			scrolls[index].SetActive(false);
			UpdateScrollsCount();
			currentActiveScrollIndex = SetCurrentIndex();
		}
	}

	int SetCurrentIndex()
	{
		bool isSet = false;
		int j = 0;

		if (scrollsCount == 0) {
			myAnimator.enabled = false;
			return -1;
		}
		else {
			for (int i = 0; i<scrolls.Length; ++i) {
				if (scrolls [i].activeInHierarchy && !isSet) {
					j = i;
					isSet = true;
				}
			}
			return j;
		}
	}

	void NextAvailableScroll()
	{
		bool isFound = false;
		int i = currentActiveScrollIndex;

		if (++i <= scrolls.Length - 1 && !isFound) {
			if (scrolls [i].activeInHierarchy) {
				currentActiveScrollIndex = i;
				isFound = true;
			}
			else
			{
				for(int j = i; j<scrolls.Length; ++j)
				{
					if(scrolls[j].activeInHierarchy && !isFound)
					{
						currentActiveScrollIndex = j;
						isFound = true;
					}
				}
			}
		} else {
			i = scrolls.Length - 1;
			currentActiveScrollIndex = scrolls.Length-1;
		}
	}

	void PreviousAvailableScroll()
	{
		bool isFound = false;
		int i = currentActiveScrollIndex;
		
		if (--i >= 0 && !isFound) {
			if (scrolls [i].activeInHierarchy) {
				currentActiveScrollIndex = i;
				isFound = true;
			}
			else
			{
				for(int j = i; j>=0; --j)
				{
					if(scrolls[j].activeInHierarchy && !isFound)
					{
						currentActiveScrollIndex = j;
						isFound = true;
					}
				}
			}
		} else {
			i = 0;
			currentActiveScrollIndex = 0;
		}
	}

	public void OnEndEdit()
	{
		scrolls [currentActiveScrollIndex].GetComponentInChildren<InputField> ().DeactivateInputField();
	}
}
