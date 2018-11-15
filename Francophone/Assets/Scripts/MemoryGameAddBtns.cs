using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameAddBtns : MonoBehaviour {

    [SerializeField]
    private Transform MemoryPanel;

    [SerializeField]
    private GameObject btn;

    void Awake()
    {
        for(int i=0;i < 10; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(MemoryPanel);
        }
    }
}
