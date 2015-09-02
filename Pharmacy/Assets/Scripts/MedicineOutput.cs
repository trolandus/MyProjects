using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class MedicineOutput : MonoBehaviour
{
    public List<Medicine> medicines;

	// Use this for initialization
	void Start () {
	    medicines = new List<Medicine>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Medicine")
        {
            Medicine m = col.GetComponent<Medicine>();
            if (!m.isDragged && !medicines.Contains(m))
            {
                medicines.Add(m);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Medicine")
        {
            Medicine m = col.GetComponent<Medicine>();
            if (medicines.Contains(m))
            {
                medicines.Remove(m);
            }
        }
    }

    public void SellMedicine()
    {
        foreach (Medicine m in medicines)
        {
            medicines.Remove(m);
            GameObject.Destroy(m.gameObject);
        }
        //Check conformity to client's order
        //...
        //Sum up profits
        //...
    }
}
