using UnityEngine;
using System.Collections;

public class WeaponElement : MonoBehaviour {

	public string stat;
	public UnityEngine.UI.Text statText;

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
		statText.text = stat;
		statText.transform.position = ps.transform.position;
		statText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		statText.transform.position = ps.transform.position;
	}
}
