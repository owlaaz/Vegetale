using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private static Inventory s_instance;
    public static Inventory Instance
    {
        get
        {
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance != null) Debug.LogError("Inventory already exists.");
        s_instance = this;
    }

    private void OnDestroy()
    {
        if (s_instance == this) s_instance = null;
    }



    private List<Tools> listOfTools;
    private List<Plant> listOfPlant;
    private List<Item> listOfItem;
    private float money;
    
    void Start()
    {
        loadJson();
    }

    public void loadJson()
    {
        listOfTools = Data.loadToolsItem();
        listOfPlant = Data.loadPlantItem();
        listOfItem = Data.loadItem();
        money = Data.loadMoney();
    }
}
