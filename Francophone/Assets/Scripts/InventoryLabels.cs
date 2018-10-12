using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLabels : MonoBehaviour {

    public Button btn;
    public Text txtQuantity;
    public Text txtItemName;
    public Image img;

    public Sprite[] potionImages; //Enter potion images on ItemBtn Prefab under Potion Images array
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
        img.sprite = potionImages[Item.ImageNumber]; 

        Item.MarkedForDelete = false;

    }

    public void HandlePress()
    {
        Item.GetType().GetMethod(Item.ItemName+"Potion").Invoke(Item, null);
        Item.Quantity--;
        txtQuantity.text = Item.Quantity + "";
        if (Item.Quantity <= 0)
        {
            Item.MarkedForDelete = true;
        }
    }

    
}
