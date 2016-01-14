using UnityEngine;
using System.Collections;

public enum StatType { DAMAGE, SKILL };

public class WeaponElement : MonoBehaviour {

	public string stat;
	public StatType statType;
	public UnityEngine.UI.Text statText;
    public Canvas canvas;
    public bool isLeft;
    public Camera cameraToLookAt;

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
		statText.text = stat;
	    statText.transform.position = this.transform.position;//textPos.transform.position;// + new Vector3(0.5f, 0.0f, 0.0f);
		statText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if(!isLeft)
	    //Vector3 pos = new Vector3(0.5f, 0.0f, 0.0f);

        canvas.transform.LookAt(cameraToLookAt.transform.position);
        canvas.transform.rotation = cameraToLookAt.transform.rotation;
	    statText.transform.position = this.transform.position;// + Camera.main.WorldToScreenPoint(pos);// + new Vector3(0.5f, 0.0f, 0.0f);
        Debug.Log(statText.transform.position);
	    statText.rectTransform.anchoredPosition = new Vector2(statText.rectTransform.anchoredPosition.x + 3.5f, statText.rectTransform.anchoredPosition.y);
	    //statText.transform.LookAt(cameraToLookAt.transform.position);
	    //statText.transform.rotation = cameraToLookAt.transform.rotation;
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

    public ParticleSystem GetParticleSystem()
    {
        return ps;
    }
}
