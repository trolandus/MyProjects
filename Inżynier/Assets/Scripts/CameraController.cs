using UnityEngine;
using System.Collections;

//OGRANICZENIA GÓRA-DÓŁ
//PRZY CHODZENIU POSTAĆ IDZIE W KIERUNKU PATRZENIA KAMERY
//PRZY STANIU KAMERA OBRACA SIĘ WOKÓŁ POSTACI

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float distanceAway;
	public float distanceUp;
	public float smooth;

	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {

	}
	
	// LateUpdate is called once per frame after Update has finished
	void LateUpdate () {
		targetPosition = target.transform.position + target.transform.up * distanceUp - target.transform.forward * distanceAway;

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);

		transform.LookAt (target.transform);
	}
}
