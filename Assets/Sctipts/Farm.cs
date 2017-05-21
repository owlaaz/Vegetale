using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    private static Farm s_instance;
    public static Farm Instance
    {
        get
        {
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance != null) Debug.LogError("Farm already exists.");
        s_instance = this;
    }

    private void OnDestroy()
    {
        if (s_instance == this) s_instance = null;
    }

    // Use this for initialization
    public Garden[] m_gardens;

    void Start () {
        //Plant[] json_plant = new Plant[3];
        //GameManager.Instance.loadJson(ref json_plant, "vegetable");
        //print(json_plant[0].getItemName());
        //print(json_plant[1].getItemName());
        //print(json_plant[2].getItemName());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextDay()
    {
        // growth up all gardent
        foreach(Garden garden in m_gardens)
        {
            garden.growthUp();
        }
        //sell crop

        //check quest
    }

    public void sellCrop()
    {

    }
}
