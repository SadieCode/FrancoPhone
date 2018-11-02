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

    public void ConvertingWordsToPower()
    {
        //Quest item, gets removed by storyline
        Quantity++;
    }

    public void StrangePotion()
    {
        GameObject player = GameObject.Find("Player");
        if( player != null )
        {
            Vector2 teleportLocation = new Vector2(-4, 2);
            Transform target = player.transform;
            target.position = Vector2.MoveTowards(target.position, teleportLocation, 10000);
        }
    }
}
