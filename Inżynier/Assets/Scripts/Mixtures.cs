using UnityEngine;
using System.Collections;

public class Mixtures : MonoBehaviour {

	private Vector3 oldPosition;
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

	// Use this for initialization
	void Start () {
		oldParent = this.transform.parent;
		oldPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
