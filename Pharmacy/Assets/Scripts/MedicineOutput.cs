using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MedicineOutput : Singleton<MedicineOutput>
{
    public List<Medicine> medicines;
    public Button sellButton;
    public Client currentClient;

    protected MedicineOutput() { }

	// Use this for initialization
	void Start ()
	{
        medicines = new List<Medicine>();
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
        int med = 0;

        //Check conformity to client's order
        foreach (Medicine m in medicines.ToArray())
        {
            if (MedicineDatabase.Instance.database[currentClient.trouble].Contains(m.name))
            {
                med++;
            }
        }

        //foreach (Medicine m in currentClient.orderList.ToArray())
        //{
        //    if (medicines.Find(x => x.name == m.name) != null)
        //    {
        //        med++;
        //    }
        //}

        if (med == MedicineDatabase.Instance.database[currentClient.trouble].Count)
        {
            currentClient.Positive();
        }
        else
        {
            currentClient.Negative();
        }

        foreach (Medicine m in medicines.ToArray())
        {
            medicines.Remove(m);
            m.isAdded = false;
        }


        //Sum up profits
        //...
    }
}
