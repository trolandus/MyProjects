using UnityEngine;
using System.Collections;

public enum StatType { DAMAGE, SKILL };

public class WeaponElement : MonoBehaviour {

	public string stat;
	public StatType statType;
	public UnityEngine.UI.Text statText;

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
		statText.text = stat;
		statText.transform.position = ps.transform.position + new Vector3(0.5f, 0.0f, 0.0f);
		statText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        statText.transform.position = ps.transform.position + new Vector3(0.5f, 0.0f, 0.0f);
	}

	public void SetColor(int n, Color col)
	{
		if ((n == 0 && statType == StatType.DAMAGE) ||
			(n == 1 && statType == StatType.SKILL)) {
			ps.startColor = col;
			statText.color = col;
			statText.enabled = true;
		} else {
			ps.startColor = Color.blue;
			statText.color = Color.blue;
			statText.enabled = true;
		}
	}
}
