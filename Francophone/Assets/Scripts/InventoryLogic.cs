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

            AddItem("Noon");
            AddItem("Midnight");
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

    }

    [YarnCommand("AddItemAlt")]
    public void AddItemAlt(string itemLogic)
    {
        bool existingItem = PlayerInventory.Any(item => item.ItemLogic == itemLogic);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemLogic == itemLogic);
            PlayerInventory[itemIndex].Quantity++;
        }
        else if (ItemList.Any(item => item.ItemLogic == itemLogic))
        {
            Item newItem = ItemList.Find(item => item.ItemLogic == itemLogic);
            PlayerInventory.Add(newItem);
            SortInv();
        }
        else
        {
            //Item was not found in itemList.xml
            Console.WriteLine("Item " + itemLogic + " was not found.");
        }

    }

    [YarnCommand("AddItem")]
    public void AddItem(string itemName)
    {
        /* This block is the logic for the inventory
            If the item is not in the inventory add entry
            Else increase the amount of the item

        */
        /* */
        bool existingItem = PlayerInventory.Any(item => item.ItemName == itemName);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemName == itemName);
            PlayerInventory[itemIndex].Quantity++;
        }
        else if(ItemList.Any(item => item.ItemName == itemName))
        {
            Item newItem = ItemList.Find(item => item.ItemName == itemName);
            PlayerInventory.Add(newItem);
            SortInv();
        } else
        {
            //Item was not found in itemList.xml
            Console.WriteLine("Item " + itemName + " was not found.");
        }
        /**/
    }
    
    [YarnCommand("RemoveItem")]
    public void RemoveItem(string itemName)
    {
        bool existingItem = PlayerInventory.Any(item => item.ItemName == itemName);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemName == itemName);
            PlayerInventory.RemoveAt(itemIndex);
            SortInv();
        }
    }

    [YarnCommand("RemoveItemAlt")]
    public void RemoveItemAlt(string itemLogic)
    {
        bool existingItem = PlayerInventory.Any(item => item.ItemLogic == itemLogic);
        if (existingItem)
        {
            int itemIndex = PlayerInventory.FindIndex(item => item.ItemLogic == itemLogic);
            PlayerInventory.RemoveAt(itemIndex);
            SortInv();
        }
    }


    public void SortInv()
    {
        PlayerInventory.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
    }

    public void OpenInventory()
    {
        if (InventoryPanel.activeSelf) { return; }
        RefreshDisplay();
        RemoveButtons();
        Console.WriteLine("Here");
        for(int i = 0; i < PlayerInventory.Count; i++) 
        {
            Item item = PlayerInventory[i];
            if (item.MarkedForDelete)
            {
                RemoveItem(PlayerInventory[i].ItemName);
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
        InventoryPanel.gameObject.SetActive(false);
    }

    [YarnCommand("EquipBackpack")]
    public void EquipBackPack()
    {
        btnInventory.SetActive(true);
    }

    private void RemoveButtons()
    {
        try {
            while (InventoryContent.transform.childCount > 0)
            {
                GameObject toRemove = InventoryContent.GetChild(0).gameObject;
                ItemBtnPool.ReturnObject(toRemove);
            }
        } catch
        {

        }
        
    }

    public void RefreshDisplay()
    {
        RemoveButtons();
        for (int i = 0; i < PlayerInventory.Count(); i++)
        {
            Item item = PlayerInventory[i];
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
