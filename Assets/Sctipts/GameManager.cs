using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public RectTransform gardenMenuButtonsParent;
    public RectTransform blockPanelGardenMenu;
    public RectTransform blockPanelBubblePlantMenu;
    public RectTransform blockPanelBubbleToolsMenu;
    public RectTransform blockPanelInventoryTable;
    public int currentGardenIndex;
    public int inventoryCropboxStore = 0; // 0 : inventory , 1 Cropbox , 2 Store
    public Transform[] gardenTranform;

    //public Button[] gardenMenu;
    public RectTransform[] gardenMenu;

    public Transform[] gardens;
    
    void Start () {
        ResetGame();
        StartGame();
    }

    public void StartGame()
    {
        //menuButtons.gameObject.SetActive(true);
        
    }

    public void ResetGame()
    {

    }
    IEnumerator bubbleAnimation(RectTransform[] et,int n)
    {
        int j = 0;
        foreach (RectTransform transform in et)
        {
            if (transform.gameObject.activeSelf)
            {
                transform.GetComponentInChildren<Animator>().SetTrigger("slide");
                transform.GetComponentInChildren<Animator>().transform.parent.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -45*j + 45 * (n - 1)/2 );
                yield return new WaitForSeconds(0.04f);
                j++;
            }
        }
    }

    public void gardenOnClick(int index)
    {
        int n = 0;
        // check can plant
        if (Farm.Instance.m_gardens[index].isEmpty() && Inventory.Instance.getBubbleButtonPlantList().Count > 0)
        {
            showGardenMenuButton(0, true);
            n++;
        }
            
        else
        {
            showGardenMenuButton(0, false);
        }
            
        // check can watering
        if (!Farm.Instance.m_gardens[index].isEmpty() && !Farm.Instance.m_gardens[index].isDied())
        {
            showGardenMenuButton(1, true);
            n++;
        }
            
        else
        {
            showGardenMenuButton(1, false);
        }
            
        // check can harvest
        if (!Farm.Instance.m_gardens[index].isEmpty() && Farm.Instance.m_gardens[index].isMature())
        {
            showGardenMenuButton(2, true);
            n++;
        }
            
        else
        {
            showGardenMenuButton(2, false);
        }
            
        // check can remove
        if (!Farm.Instance.m_gardens[index].isEmpty() && Farm.Instance.m_gardens[index].isDied())
        {
            showGardenMenuButton(3, true);
            n++;
        }
            
        else
        {
            showGardenMenuButton(3, false);
        }

        // check can add tools
        if(Inventory.Instance.getBubbleButtonToolsList().Count > 0)
        {
            showGardenMenuButton(4, true);
            n++;
        }
        else
        {
            showGardenMenuButton(4, false);
        }

        if (n != 0)
        {
            currentGardenIndex = index;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(gardens[index].position);

            gardenMenuButtonsParent.transform.position = screenPosition;
            gardenMenuButtonsParent.gameObject.SetActive(true);
            blockPanelGardenMenu.gameObject.SetActive(true);


            // play animation
            StartCoroutine(bubbleAnimation(gardenMenu, n));

            foreach (Transform et in gardenMenu)
            {
                et.GetComponentInChildren<EventTrigger>().triggers.Clear();
            }



            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerClick;
            entry1.callback.RemoveAllListeners();
            entry1.callback.AddListener((eventData) =>
            {
                print("Plant Button Clicked");
                // set posision
                Inventory.Instance.bubbleButtonPlantParent.transform.position = screenPosition;
                Inventory.Instance.bubbleButtonPlantParent.gameObject.SetActive(true);
                StartCoroutine(bubbleAnimation(Inventory.Instance.getBubbleButtonPlantArray(), Inventory.Instance.getBubbleButtonPlantArray().Length));
                hideGardenMenuButton();
                blockPanelBubblePlantMenu.gameObject.SetActive(true);
            });
            gardenMenu[0].GetComponentInChildren<EventTrigger>().triggers.Add(entry1);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerClick;
            entry2.callback.RemoveAllListeners();
            entry2.callback.AddListener((eventData) =>
            {
                print("Watering Button Clicked");
                hideGardenMenuButton();
            // watering
            Farm.Instance.m_gardens[index].watering();

            // change to dirt
            gardenTranform[index].GetComponent<Renderer>().material = Resources.Load<Material>("materials/garden/watering garden");
            });
            gardenMenu[1].GetComponentInChildren<EventTrigger>().triggers.Add(entry2);

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerClick;
            entry3.callback.RemoveAllListeners();
            entry3.callback.AddListener((eventData) =>
            {
                print("Harvest Button Clicked");
                hideGardenMenuButton();
                // harvest
                Farm.Instance.m_gardens[index].harvestPlant();
                // remove plant
                Farm.Instance.m_gardens[index].removePlant();
            });
            gardenMenu[2].GetComponentInChildren<EventTrigger>().triggers.Add(entry3);

            EventTrigger.Entry entry4 = new EventTrigger.Entry();
            entry4.eventID = EventTriggerType.PointerClick;
            entry4.callback.RemoveAllListeners();
            entry4.callback.AddListener((eventData) =>
            {
                print("Remove Plant Button Clicked");
                hideGardenMenuButton();
                // remove plant
                Farm.Instance.m_gardens[index].removePlant();
            });
            gardenMenu[3].GetComponentInChildren<EventTrigger>().triggers.Add(entry4);

            EventTrigger.Entry entry5 = new EventTrigger.Entry();
            entry5.eventID = EventTriggerType.PointerClick;
            entry5.callback.RemoveAllListeners();
            entry5.callback.AddListener((eventData) =>
            {
                print("Tools Button Clicked");
                Inventory.Instance.bubbleButtonToolsParent.transform.position = screenPosition;
                Inventory.Instance.bubbleButtonToolsParent.gameObject.SetActive(true);
                StartCoroutine(bubbleAnimation(Inventory.Instance.getBubbleButtonToolsArray(), Inventory.Instance.getBubbleButtonToolsArray().Length));
                hideGardenMenuButton();
                blockPanelBubbleToolsMenu.gameObject.SetActive(true);
            // tools
            //open inventory and return tools
            //Tools r_tool = Farm.Instance.Poop;

            //add to garden
            //Farm.Instance.m_gardens[i].addTool(r_tool);
            });
            gardenMenu[4].GetComponentInChildren<EventTrigger>().triggers.Add(entry5);
        }
    }

    private void showGardenMenuButton(int index,bool toggle)
    {
        gardenMenu[index].gameObject.SetActive(toggle);
    }

    public void ShowInventoryTable()
    {
        inventoryCropboxStore = 0;
        //show inventory table
        Inventory.Instance.itemInventoryParent.gameObject.SetActive(true);

        blockPanelInventoryTable.gameObject.SetActive(true);
    }
    public void showCropboxTable()
    {
        inventoryCropboxStore = 1;
        Inventory.Instance.itemInventoryParent.gameObject.SetActive(true);

        blockPanelInventoryTable.gameObject.SetActive(true);
    }
    public void showStoreTable()
    {
        inventoryCropboxStore = 2;
    }
    public void hideGardenMenuButton()
    {
        foreach(Transform p_button in gardenMenu)
        {
            if(p_button.gameObject.activeSelf == true)
            {
                p_button.GetComponentInChildren<Animator>().SetTrigger("hide");
            }
        }
        blockPanelGardenMenu.gameObject.SetActive(false);
    }
    public void hideBubblePlantButton()
    {
        foreach (Transform p_button in Inventory.Instance.getBubbleButtonPlantList())
        {
            if (p_button.gameObject.activeSelf == true)
            {
                p_button.GetComponentInChildren<Animator>().SetTrigger("hide");
            }
        }
        blockPanelBubblePlantMenu.gameObject.SetActive(false);
    }
    public void hideBubbleToolsButton()
    {
        foreach (Transform p_button in Inventory.Instance.getBubbleButtonToolsList())
        {
            if (p_button.gameObject.activeSelf == true)
            {
                p_button.GetComponentInChildren<Animator>().SetTrigger("hide");
            }
        }
        blockPanelBubbleToolsMenu.gameObject.SetActive(false);
    }
    public void hideInventoryTable()
    {
        // play animation


        // close
        Inventory.Instance.itemInventoryParent.gameObject.SetActive(false);
        blockPanelInventoryTable.gameObject.SetActive(false);
    }
}


