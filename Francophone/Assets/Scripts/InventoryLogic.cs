using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System;
using System.Xml;

public class InventoryLogic : MonoBehaviour {

    List<Item> PlayerInventory = new List<Item>();
    List<Item> ItemList;
    public Transform InventoryContent;
    public SimpleObjectPool ItemBtnPool;
    public GameObject InventoryPanel;
    public GameObject MovementUI;
    public GameObject btnPrefab;
    public GameObject btnInventory;

    // Use this for initialization
    private void Start()
    {
        InventoryPanel.gameObject.SetActive(false);

        //for future when save file is enabled
        if (ItemList == null)
        {
            InitItemList();
        }
    }

    private void InitItemList()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/ItemList", typeof(TextAsset));
        StringReader stringReader = new StringReader(textAsset.text);
        XmlTextReader reader = new XmlTextReader(stringReader);

        XmlSerializer serial = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Items"));
        ItemList = (List<Item>)serial.Deserialize(reader);
        stringReader.Close();
        reader.Close();

        ItemList.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
        /*  *
        AddItem("Word");
        AddItem("Random");
        AddItem("Random");
        /**/
        /*  To show dynamic list scrolling */
        for(int i = 0; i <= 100; i++)
        {
            AddItem("Word");
        }
        /**/
    }

    [YarnCommand("AddItem")]
    public void AddItem(string itemName)
    {
        /* To show dynamic list scrolling */
        Item newItem = ItemList.Find(item => item.ItemName == itemName);
        PlayerInventory.Add(newItem);
        SortInv();
        /**/
        /* *
        bool existingItem = PlayerInventory.Any(item => item.ItemName == itemName);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemName == itemName);
            PlayerInventory[itemIndex].Quantity++;
        }
        else
        {
            Item newItem = ItemList.Find(item => item.ItemName == itemName);
            PlayerInventory.Add(newItem);
            SortInv();
        }
        /**/
    }

    public void RemoveItem(string itemName)
    {
        bool existingItem = PlayerInventory.Any(item => item.ItemName == itemName);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemName == itemName);
            PlayerInventory.RemoveAt(itemIndex);
            SortInv();
        }
        RefreshDisplay();
    }

    public void SortInv()
    {
        PlayerInventory.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
    }

    public void OpenInventory()
    {
        MovementUI.SetActive(false);
        //RemoveButtons();
        //RefreshDisplay();
        foreach (Item item in PlayerInventory)
        {
            if (item.MarkedForDelete)
            {
                RemoveItem(item.ItemName);
                continue;
            }
            GameObject newItem = ItemBtnPool.GetObject();
            newItem.transform.SetParent(InventoryContent);
            InventoryLabels inventoryLabel = newItem.GetComponent<InventoryLabels>();
            inventoryLabel.Setup(item);
            newItem.GetComponent<RectTransform>().localScale = btnPrefab.GetComponent<RectTransform>().localScale;
        }
        InventoryPanel.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        MovementUI.SetActive(true);
        InventoryPanel.gameObject.SetActive(false);
    }

    [YarnCommand("EquipBackpack")]
    public void EquipFrancophone()
    {
        btnInventory.SetActive(true);
    }

    private void RemoveButtons()
    {
        //Infinite loop, need to fix
        while (InventoryContent.childCount > 0)
        {
            GameObject toRemove = InventoryContent.GetChild(0).gameObject;
            ItemBtnPool.ReturnObject(toRemove);
        }
    }

    public void RefreshDisplay()
    {
        //RemoveButtons();
        foreach (Item item in PlayerInventory)
        {
            if (item.MarkedForDelete)
            {
                RemoveItem(item.ItemName);
                continue;
            }
            GameObject newItem = ItemBtnPool.GetObject();
            newItem.transform.SetParent(InventoryContent);
            InventoryLabels inventoryLabel = newItem.GetComponent<InventoryLabels>();
            inventoryLabel.Setup(item);
        }
    } 
}
