using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLabels : MonoBehaviour {

    public Button btn;
    public Text txtQuantity;
    public Text txtItemName;
    public Image img;

    public Sprite[] itemImages; //Enter item images on ItemBtn Prefab under Potion Images array
    private Item Item;

    // Use this for initialization
    void Start () {
        btn.onClick.AddListener(HandlePress);
	}

    public void Setup(Item currentItem)
    {
        Item = currentItem;
        txtItemName.text = Item.ItemName;
        txtQuantity.text = Item.Quantity+"";
        img.sprite = itemImages[Item.ImageNumber]; 

        Item.MarkedForDelete = false;

    }

    public void HandlePress()
    {
        if (Item.Quantity > 0)
        {
            Item.GetType().GetMethod(Item.ItemLogic).Invoke(Item, null);
            Item.Quantity--;
        }
        txtQuantity.text = Item.Quantity + "";
        if (Item.Quantity <= 0)
        {
            Item.MarkedForDelete = true;
        }
    }

    
}
