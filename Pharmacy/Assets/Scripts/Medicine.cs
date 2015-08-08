using System;
using UnityEngine;
using System.Collections;

public class Medicine : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    DragDrop();
	}

    void DragDrop()
    {
        if (DetectMouse())
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vec3 = Input.mousePosition;
                vec3.z = 10.0f;
                vec3 = Camera.main.ScreenToWorldPoint(vec3);
                this.transform.position = vec3;
            }
        }
    }

    bool DetectMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
