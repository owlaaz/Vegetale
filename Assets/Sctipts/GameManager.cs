using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager s_instance;
    public static GameManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance != null) Debug.LogError("GameManager already exists.");
        s_instance = this;
    }

    private void OnDestroy()
    {
        if (s_instance == this) s_instance = null;
    }

    public Button[] m_menuButton;
    
    void Start () {
        ResetGame();
        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {

    }

    public void ResetGame()
    {

    }

    //public void nextDay()
    //{
    //    Farm.Instance.nextDay();
    //}

    //public void watering(int index)
    //{
    //    Farm.Instance.m_gardens[index].watering();
    //}

    //public void sellCrop()
    //{
    //    Farm.Instance.sellCrop();
    //}
    
    public void gardenOnClick(int index)
    {
        foreach(Button b in m_menuButton)
        {
            b.onClick.RemoveAllListeners();
        }

        int i = index;
        m_menuButton[0].onClick.AddListener(() =>
        {
            print("Plant Button Clicked");
            // plant

            //open inventory and return plant
            //Plant r_plant = Farm.Instance.tomato;

            // plant
            //Farm.Instance.m_gardens[i].plant(r_plant);
        });
        m_menuButton[1].onClick.AddListener(() =>
        {
            print("Watering Button Clicked");
            // watering
            Farm.Instance.m_gardens[i].watering();
        });
        m_menuButton[2].onClick.AddListener(() =>
        {
            print("Harvest Button Clicked");
            // harvest
        });
        m_menuButton[3].onClick.AddListener(() =>
        {
            print("Remove Plant Button Clicked");
            // remove
            Farm.Instance.m_gardens[i].removePlant();
        });
        m_menuButton[4].onClick.AddListener(() =>
        {
            print("Tools Button Clicked");
            // tools
            //open inventory and return tools
            //Tools r_tool = Farm.Instance.Poop;

            //add to garden
            //Farm.Instance.m_gardens[i].addTool(r_tool);
        });
    }



}


