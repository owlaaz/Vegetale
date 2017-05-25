using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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


    private List<Plant> listOfPlant = new List<Plant>();
    private List<Tools> listOfTools = new List<Tools>();
    private List<Item> listOfItem = new List<Item>();
    private float money;

    private List<RectTransform> bubbleButtonPlantList = new List<RectTransform>();
    private List<RectTransform> bubbleButtonToolsList = new List<RectTransform>();
    private List<RectTransform> bubbleButtonItemList = new List<RectTransform>();

    public RectTransform bubbleButtonPrefab;
    public RectTransform buttonItemInvertoryPrefab;

    public RectTransform bubbleButtonPlantParent;
    public RectTransform bubbleButtonToolsParent;
    public RectTransform bubbleButtonItemParent;
    public RectTransform itemInventoryParent;

    void Start()
    {
        loadJson();
        initBubbleMenu();
        initInventoryTable();

        Inventory.Instance.addItemToList(2001, 2);
        Inventory.Instance.addItemToList(2002, 1);
    }

    public void loadJson()
    {
        listOfPlant = Data.loadPlantItem();
        listOfTools = Data.loadToolsItem();
        listOfItem = Data.loadItem();
        money = Data.loadMoney();

    }

    public bool addItemToList(int p_itemID,int n)
    {
        
        foreach(Plant item in listOfPlant)
        {
            if (item.getItemID() == p_itemID)
            {
                
                item.addItem(n);
                updateQuantityItem(item);
                if (item.getQuantity() == n)
                {
                    updateAddToInventory(item);
                    addBubblePlantButton(item);
                }
                return true;
            }
        }
        foreach (Tools item in listOfTools)
        {
            if (item.getItemID() == p_itemID)
            {
                item.addItem(n);
                updateQuantityItem(item);
                if (item.getQuantity() == n)
                {
                    updateAddToInventory(item);
                    addBubbleToolsButton(item);
                }
                return true;
            }
        }
        foreach (Item item in listOfItem)
        {
            if (item.getItemID() == p_itemID)
            {
                item.addItem(n);
                updateQuantityItem(item);
                if (item.getQuantity() == n)
                {
                    updateAddToInventory(item);
                    addBubbleItemButton(item);
                }
                return true;
            }
        }
        return false;
    }

    public bool removeItemFromList(int p_itemID, int n)
    {
        foreach (Plant item in listOfPlant)
        {
            if (item.getItemID() == p_itemID && item.removeItem(n) && updateRemoveFromInventory(item) && item.getQuantity() == 0)
            {
                removeBubblePlantButton(item);
                return true;
            }
        }
        foreach (Tools item in listOfTools)
        {
            if (item.getItemID() == p_itemID && item.removeItem(n) && updateRemoveFromInventory(item) && item.getQuantity() == 0)
            {
                removeBubbleToolsButton(item);
                return true;
            }
        }
        foreach (Item item in listOfItem)
        {
            if (item.getItemID() == p_itemID && item.removeItem(n) && updateRemoveFromInventory(item) && item.getQuantity() == 0)
            {
                removeBubbleItemButton(item);
                return true;
            }
        }
        return false;
    }

    public bool updateRemoveFromInventory(Item p_item)
    {
        for(int i = 0; i < itemInventoryParent.childCount; i++)
        {
            if(p_item.getItemName() == itemInventoryParent.GetChild(i).name)
            {
                itemInventoryParent.GetChild(i).GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
                if(p_item.getQuantity() <= 0)
                {
                    Destroy(itemInventoryParent.GetChild(i).gameObject);
                }
                return true;
            }
        }
        return false;
    }
    public bool updateAddToInventory(Plant p_item)
    {
        print("Plant");
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/seeds/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
        return true;
    }
    public bool updateAddToInventory(Tools p_item)
    {
        print("Tools");
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/tools/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
        return true;
    }
    public bool updateAddToInventory(Item p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/crops/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
        return true;
    }
    public bool updateQuantityItem(Item p_item)
    {
        for (int i = 0; i < itemInventoryParent.childCount; i++)
        {
            if (p_item.getItemName() == itemInventoryParent.GetChild(i).name)
            {
                itemInventoryParent.GetChild(i).GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
                return true;
            }
        }
        return false;
    }

    public void initBubbleMenu()
    {
        foreach (Plant item in listOfPlant)
        {

            if (item.getQuantity() > 0)
            {
                addBubblePlantButton(item);
            }

        }
        foreach (Tools item in listOfTools)
        {

            if (item.getQuantity() > 0)
            {
                addBubbleToolsButton(item);
            }

        }
        foreach (Item item in listOfItem)
        {
            if (item.getQuantity() > 0)
            {
                addBubbleItemButton(item);
            }
        }
    }
    public void initInventoryTable()
    {
        foreach (Plant item in listOfPlant)
        {

            if (item.getQuantity() > 0)
            {
                addItemToInventory(item);
            }

        }
        foreach (Tools item in listOfTools)
        {

            if (item.getQuantity() > 0)
            {
                addItemToInventory(item);
            }

        }
        foreach (Item item in listOfItem)
        {
            if (item.getQuantity() > 0)
            {
                addItemToInventory(item);
            }
        }
    }

    public void addItemToInventory(Plant p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/seeds/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
    }
    public void addItemToInventory(Tools p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/tools/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
    }
    public void addItemToInventory(Item p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(buttonItemInvertoryPrefab, itemInventoryParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/crops/" + p_item.getItemName());
        btn.GetChild(1).GetComponent<Text>().text = p_item.getQuantity() + "";
    }

    public RectTransform[] getBubbleButtonPlantArray()
    {
        return bubbleButtonPlantList.ToArray();
    }
    public RectTransform[] getBubbleButtonToolsArray()
    {
        return bubbleButtonToolsList.ToArray();
    }
    public RectTransform[] getBubbleButtonItemArray()
    {
        return bubbleButtonItemList.ToArray();
    }
    public List<RectTransform> getBubbleButtonPlantList()
    {
        return bubbleButtonPlantList;
    }
    public List<RectTransform> getBubbleButtonToolsList()
    {
        return bubbleButtonToolsList;
    }
    public List<RectTransform> getBubbleButtonItemList()
    {
        return bubbleButtonItemList;
    }
    
    public void addBubblePlantButton(Plant p_item)
    {

        // generate prefap
        RectTransform btn = Instantiate(bubbleButtonPrefab, bubbleButtonPlantParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        //btn.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("images/seeds/" + p_item.getItemName());
        btn.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/seeds/" + p_item.getItemName());

        // add event to each button
        EventTrigger.Entry e = new EventTrigger.Entry();
        e.eventID = EventTriggerType.PointerClick;
        e.callback.RemoveAllListeners();
        e.callback.AddListener((evetnData) =>
        {
            GameManager.Instance.hideBubblePlantButton();
            // garden.plant(plant)
            Farm.Instance.m_gardens[GameManager.Instance.currentGardenIndex].plant(p_item);
            // comsume plane quantity - 1
            removeItemFromList(p_item.getItemID(), 1);
            // change image gaden
            GameManager.Instance.gardenTranform[GameManager.Instance.currentGardenIndex].GetChild(0).gameObject.SetActive(true);

        });
        btn.GetComponentInChildren<EventTrigger>().triggers.Add(e);
        
        bubbleButtonPlantList.Add(btn);
    }
    public void addBubbleToolsButton(Tools p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(bubbleButtonPrefab, bubbleButtonToolsParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        btn.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("images/tools/" + p_item.getItemName());

        // add event to each button
        EventTrigger.Entry e = new EventTrigger.Entry();
        e.eventID = EventTriggerType.PointerClick;
        e.callback.RemoveAllListeners();
        e.callback.AddListener((evetnData) =>
        {
            GameManager.Instance.hideBubbleToolsButton();
            Farm.Instance.m_gardens[GameManager.Instance.currentGardenIndex].addTool(p_item);
            // comsume tools quantity - 1
            removeItemFromList(p_item.getItemID(), 1);
        });
        btn.GetComponentInChildren<EventTrigger>().triggers.Add(e);
        

        bubbleButtonToolsList.Add(btn);
    }
    public void addBubbleItemButton(Item p_item)
    {
        // generate prefap
        RectTransform btn = Instantiate(bubbleButtonPrefab, bubbleButtonItemParent);
        btn.gameObject.name = p_item.getItemName();
        // change image prefab
        //btn.GetComponentInChildren<Image>().sprite = p_item.imagePath();

        // add event to each button
        EventTrigger.Entry e = new EventTrigger.Entry();
        e.eventID = EventTriggerType.PointerClick;
        e.callback.RemoveAllListeners();
        e.callback.AddListener((evetnData) =>
        {
            //item detail

        });
        btn.GetComponentInChildren<EventTrigger>().triggers.Add(e);
        
        bubbleButtonItemList.Add(btn);
    }

    public bool removeBubblePlantButton(Plant p_item)
    {
        //remove from game
        foreach (RectTransform bubbleButton in bubbleButtonPlantList)
        {
            if (p_item.getItemName() == bubbleButton.gameObject.name)
            {
                bubbleButtonPlantList.Remove(bubbleButton);
                Destroy(bubbleButton.gameObject);
                return true;
            }
        }
        return false;
    }
    public bool removeBubbleToolsButton(Tools p_item)
    {
        //remove from game
        foreach (RectTransform bubbleButton in bubbleButtonToolsList)
        {
            if (p_item.getItemName() == bubbleButton.gameObject.name)
            {
                bubbleButtonToolsList.Remove(bubbleButton);
                Destroy(bubbleButton.gameObject);
                return true;
            }
        }
        return false;
    }
    public bool removeBubbleItemButton(Item p_item)
    {
        //remove from game
        foreach (RectTransform bubbleButton in bubbleButtonItemList)
        {
            if (p_item.getItemName() == bubbleButton.gameObject.name)
            {
                bubbleButtonItemList.Remove(bubbleButton);
                Destroy(bubbleButton.gameObject);
                return true;
            }
        }
        return false;
    }
}
