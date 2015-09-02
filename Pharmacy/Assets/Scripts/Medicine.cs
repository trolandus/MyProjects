﻿using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    public string name;
    public bool isDragged;
    public Text text;

    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
	    text.text = name;
	    text.enabled = false;
	    isDragged = false;
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
                text.enabled = false;
                isDragged = true;
                Vector3 vec3 = Input.mousePosition;
                vec3.z = 10.0f;
                vec3 = Camera.main.ScreenToWorldPoint(vec3);
                this.transform.position = vec3;
            }
            else
            {
                isDragged = false;
            }
        }
    }

    bool DetectMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Medicine")
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    text.GetComponentInParent<Canvas>().transform as RectTransform, Input.mousePosition,
                    text.GetComponentInParent<Canvas>().worldCamera, out pos);
                text.transform.position = text.GetComponentInParent<Canvas>().transform.TransformPoint(pos);
                text.enabled = true;
                return true;   
            }
            else
            {
                return false;
            }
        }
        else
        {
            text.enabled = false;
            return false;
        }
    }

}
