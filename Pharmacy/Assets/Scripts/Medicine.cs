using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    public string name;
    public bool isAdded;

    private Ray ray;
    private RaycastHit hit;
    private Vector3 startPosition;

	// Use this for initialization
	void Start ()
	{
	    isAdded = false;
	    startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(!isAdded)
            this.transform.position = startPosition;
	}

    public void GetMedicine()
    {
        if (!isAdded)
        {
            this.transform.position = MedicineOutput.Instance.transform.position;
            MedicineOutput.Instance.medicines.Add(this);
            isAdded = true;   
        }
    }

    void OnMouseDown()
    {
        if (isAdded)
        {
            MedicineOutput.Instance.medicines.Remove(this);
            isAdded = false;
        }
    }

}
