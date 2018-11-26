using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Item {

    DictionaryLogic dictionaryLogic;

    public string ItemLogic { get; set; }

    public string ItemName { get; set; }

    public int Quantity { get; set; }

    public int ImageNumber { get; set; }

    public int MetaData { get; set; }

    public bool MarkedForDelete { get; set; }
    //public GameObject ItemLogic;
    public Item()
    {
    }
    
    public void TimePotion()
    {
        TimeLogic.hour = MetaData;
    }

    public void ConvertingWordsToPower()
    {
        //Quest item, gets removed by storyline
        Quantity++;
    }
}
