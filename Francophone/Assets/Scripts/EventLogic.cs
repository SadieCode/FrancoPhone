using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLogic : MonoBehaviour {

    public GameObject chat;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        //Quest 0
        if(!chat.activeSelf && TimeLogic.hour >= 1 && TimeLogic.hour < 5)
        {
            chat.SetActive(true);
        }

        if (chat.activeSelf && TimeLogic.hour > 4)
        {
            chat.SetActive(false);
        }
    }
}
