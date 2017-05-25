using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Garden
{
    private Transform gardenTranfrom;
    [SerializeField]
    private bool empty = true;
    [SerializeField]
    private Plant m_plant = null;
    [SerializeField]
    private int plantDay=0;
    [SerializeField]
    private int dayToGrowth=0;
    [SerializeField]
    private int dayToDied=0;
    [SerializeField]
    private List<Tools> listOfTool = new List<Tools>();
    [SerializeField]
    private bool water=false;

    public Garden()
    {
        //empty = true;
        //m_plant = null;
        //plantDay = 0;
        //dayToGrowth = 0;
        //dayToDied = 0;
        //listOfTool = new List<Tools>();
        //water = false;
    }

    public void plant(Plant p_plant)
    {
        m_plant = p_plant;
        empty = false;
        plantDay = 0;
        dayToGrowth = p_plant.getDayToGrowth(0);
        dayToDied = p_plant.getDayToDied(0);
    }
    public void addTool(Tools p_tool)
    {
        listOfTool.Add(p_tool);
    }
    public void watering()
    {
        water = true;
    }
    public void growthUp()
    {
        int get_level = m_plant.getPlantLevel();
        plantDay++;
        foreach(Tools pop_tool in listOfTool)
        {
            dayToGrowth -= pop_tool.getEffectToGrowth();
            dayToDied -= pop_tool.getEffectToDied();
        }
        listOfTool.Clear();
        if (!m_plant.isMature())
        {
            dayToDied--;
            if (dayToDied <= 0)
            {
                //hide seed
                gardenTranfrom.GetChild(0).gameObject.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //change image
                        gardenTranfrom.GetChild(i * 3 + j + 1).GetComponent<Renderer>().material = Resources.Load<Material>("materials/plants/died plant");
                        //show poop
                        gardenTranfrom.GetChild(i * 3 + j + 1).gameObject.SetActive(true);
                        // rescale
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localScale = new Vector3(0.25f, 1, 0.1f);
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localPosition = new Vector3((i - 1) * 3, 0.2f, (j - 1) * 3);
                    }
                }
                m_plant.died();
            }
                
            if (water == true) dayToGrowth--;
            if (dayToGrowth <= 0 && !isDied())
            {
                m_plant.growth();
                int new_level = m_plant.getPlantLevel();

                dayToGrowth = m_plant.getDayToGrowth(new_level);
                dayToDied = m_plant.getDayToDied(new_level);

                for(int i=0;i<3;i++)
                {
                    for(int j=0;j<3;j++)
                    {
                        //garden change image
                        gardenTranfrom.GetChild(i * 3 + j + 1).GetComponent<Renderer>().material = Resources.Load<Material>("materials/plants/" + m_plant.getItemName().Replace(" seed", "") + " plant");

                        // set plant active true
                        gardenTranfrom.GetChild(i * 3 + j + 1).gameObject.SetActive(true);

                        // change scale
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localScale = new Vector3(0.4f / m_plant.getMaxLevel() * m_plant.getPlantLevel(), 1, 0.16f / m_plant.getMaxLevel() * m_plant.getPlantLevel());
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localPosition = new Vector3((i - 1) * 3, 0.8f / m_plant.getMaxLevel() * m_plant.getPlantLevel(), (j - 1) * 3);
                    }
                }

                // hide seed
                gardenTranfrom.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            if (dayToDied <= 0)
            {
                //hide seed
                gardenTranfrom.GetChild(0).gameObject.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //change image
                        gardenTranfrom.GetChild(i * 3 + j + 1).GetComponent<Renderer>().material = Resources.Load<Material>("materials/plants/died plant");
                        //show poop
                        gardenTranfrom.GetChild(i * 3 + j + 1).gameObject.SetActive(true);
                        // rescale
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localScale = new Vector3(0.25f, 1, 0.1f);
                        gardenTranfrom.GetChild(i * 3 + j + 1).transform.localPosition = new Vector3((i - 1) * 3, 0.2f, (j - 1) * 3);
                    }
                }
                m_plant.died();
            }
            dayToDied--;
            if (water == true && !isDied()) dayToDied = m_plant.getDayToDied(get_level);

        }
        water = false;
        // dry dirt
        gardenTranfrom.GetComponent<Renderer>().material = Resources.Load<Material>("materials/garden/dry garden");
    }
    public void harvestPlant()
    {
        Inventory.Instance.addItemToList(m_plant.getItemID() + 2000, 9);
    }
    public void removePlant()
    {
        // set gardet empty
        empty = true;

        // remove seed
        gardenTranfrom.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // hide plant
                gardenTranfrom.GetChild(i * 3 + j + 1).gameObject.SetActive(false);
            }
        }
    }
    public bool isEmpty()
    {
        return empty;
    }
    public bool isMature()
    {
        return m_plant.isMature();
    }
    public void setGardenTransform(Transform p_transform)
    {
        gardenTranfrom = p_transform;
    }
    public bool isDied()
    {
        if (m_plant.getPlantLevel() < 0 || dayToDied < 0)
        {
            return true;
        }
        return false;
    }
}
