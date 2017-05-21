using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField]
    protected int itemID;
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected string itemDetail;
    [SerializeField]
    protected int itemType;
    [SerializeField]
    protected double itemPrice;
    [SerializeField]
    protected int itemQuantity;

}

[Serializable]
public class Plant : Item
{
    [SerializeField]
    private int plantDay;
    [SerializeField]
    private int plantLevel;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private int[] dayToGrowth = { 1, 3, 5, 0 };
    [SerializeField]
    private int[] dayToDied = { 2, 4, 6, 3 };
    public bool isMature()
    {
        if (plantLevel == maxLevel)
            return true;
        return false;
    }
    public int getPlantLevel()
    {
        return plantLevel;
    }
    public bool growth()
    {
        if (plantLevel < maxLevel)
        {
            plantLevel++;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool died()
    {
        if (plantLevel > 0)
        {
            plantLevel *= -1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public int getDayToGrowth(int p_level)
    {
        return dayToGrowth[p_level];
    }
    public int getDayToDied(int p_level)
    {
        return dayToDied[p_level];
    }

    public string getAll()
    {
        return this;
    }
}

[Serializable]
public class Tools : Item
{
    [SerializeField]
    private int toolType;
    [SerializeField]
    private int effectToGrowth;
    [SerializeField]
    private int effectToDied;

    public int getEffectToGrowth()
    {
        return effectToGrowth;
    }
    public int getEffectToDied()
    {
        return effectToDied;
    }
}
