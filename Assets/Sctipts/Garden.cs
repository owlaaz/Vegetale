using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Garden
{

    [SerializeField]
    private bool empty;
    [SerializeField]
    private Plant m_plant;
    [SerializeField]
    private int plantDay;
    [SerializeField]
    private int dayToGrowth;
    [SerializeField]
    private int dayToDied;
    [SerializeField]
    private List<Tools> listOfTool;
    [SerializeField]
    private bool water;

    public Garden()
    {
        empty = true;
        m_plant = null;
        plantDay = 0;
        dayToGrowth = 0;
        dayToDied = 0;
        listOfTool = new List<Tools>();
        water = false;
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
        if (listOfTool.Count > 0)
        {
            Tools pop_tool = listOfTool[listOfTool.Count - 1];
            do
            {
                dayToGrowth -= pop_tool.getEffectToGrowth();
                dayToDied -= pop_tool.getEffectToDied();
                listOfTool.Remove(pop_tool);
                pop_tool = listOfTool[listOfTool.Count - 1];
            } while (listOfTool.Count > 0);
        }
        if (!m_plant.isMature())
        {
            dayToDied--;
            if (dayToDied <= 0)
                m_plant.died();
            if (water == true) dayToGrowth--;
            if (dayToGrowth <= 0)
            {
                m_plant.growth();
                int new_level = m_plant.getPlantLevel();

                dayToGrowth = m_plant.getDayToGrowth(new_level);
                dayToDied = m_plant.getDayToDied(new_level);
            }
        }
        else
        {
            if (dayToDied <= 0)
                m_plant.died();
            dayToDied--;
            if (water == true) dayToDied = m_plant.getDayToDied(get_level);

        }
        water = false;
    }
    public void removePlant()
    {
        empty = true;
    }
}
