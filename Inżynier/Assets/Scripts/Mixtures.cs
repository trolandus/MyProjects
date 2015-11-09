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

	// Use this for initialization
	void Start () {
		oldParent = this.transform.parent;
		oldPosition = this.transform.localPosition;
		oldRotation = this.transform.localRotation;
		oldScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
