using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    public string name;
    public MedicineOutput output;
    public bool isAdded;

    private Ray ray;
    private RaycastHit hit;
    private GameObject medCopy;
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
            this.transform.position = output.transform.position;
            output.medicines.Add(this.gameObject);
            isAdded = true;   
        }
    }

    void OnMouseDown()
    {
        if (isAdded)
        {
            output.medicines.Remove(this.gameObject);
            isAdded = false;
        }
    }

}
