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

    void Start() {
        m_gardens = new Garden[] { new Garden(), new Garden(), new Garden()};
        m_gardens[0].setGardenTransform(GameManager.Instance.gardens[0]);
        m_gardens[1].setGardenTransform(GameManager.Instance.gardens[1]);
        m_gardens[2].setGardenTransform(GameManager.Instance.gardens[2]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextDay()
    {
        // growth up all gardent
        foreach(Garden garden in m_gardens)
        {
            if(!garden.isEmpty() && !garden.isDied())
            garden.growthUp();
        }
    }

    public void sellCrop()
    {

    }
}
