using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedicineDatabase : Singleton<MedicineDatabase>
{

    protected MedicineDatabase() { }

    public Dictionary<string, List<string>> database;

	// Use this for initialization
	void Start () {
        database = new Dictionary<string, List<string>>();
	    database.Add("Headache", new List<string>(){"Pills"});
        database.Add("Fever", new List<string>(){"Tablette"});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
