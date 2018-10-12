using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    DictionaryLogic dictionaryLogic;

    public string ItemName { get; set; }

    public int Quantity { get; set; }

    public int ImageNumber { get; set; }

    public bool MarkedForDelete { get; set; }
    //public GameObject ItemLogic;
    public Item()
    {
    }
    
    public void WordPotion()
    {
        dictionaryLogic = GameObject.Find("GENERAL").GetComponent<DictionaryLogic>();
        dictionaryLogic.AddWord("Rouge");
    }

    public void RandomPotion()
    {
        
    }
}
