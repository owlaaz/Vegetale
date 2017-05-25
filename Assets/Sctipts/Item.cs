using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    public string getItemName()
    {
        return itemName;
    }
    public int getItemID()
    {
        return itemID;
    }
    public int getQuantity()
    {
        return itemQuantity;
    }
    public void addItem(int n)
    {
        itemQuantity += n;
    }
    public bool removeItem(int n)
    {
        if (itemQuantity - n >= 0) 
        {
            itemQuantity -= n;
            return true;
        }
        return false;
        
    }
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
    private int[] dayToGrowth;
    [SerializeField]
    private int[] dayToDied;
    public bool isMature()
    {
        if (plantLevel == maxLevel)
            return true;
        return false;
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
    public int getPlantLevel()
    {
        return plantLevel;
    }
    public int getMaxLevel()
    {
        return maxLevel;
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
