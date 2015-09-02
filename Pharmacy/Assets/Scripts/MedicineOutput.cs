using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

public class MedicineOutput : MonoBehaviour
{
    public List<GameObject> medicines;
    public Button sellButton;

	// Use this for initialization
	void Start ()
	{
	    medicines = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (medicines.Count == 0)
	        sellButton.interactable = false;
	    else
	    {
	        sellButton.interactable = true;
	    }
	}

    public void SellMedicine()
    {
        foreach (GameObject m in medicines.ToArray())
        {
            medicines.Remove(m);
            m.GetComponent<Medicine>().isAdded = false;
        }
        //Check conformity to client's order
        //...
        //Sum up profits
        //...
    }
}
