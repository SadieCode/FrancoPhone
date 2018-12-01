using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour {

    public GameObject panel;
    //public GameObject panelSchool;

    public GameObject player;
    private Vector3 fastTravelPosition;
    public Transform target;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerLogic>() != null)
        {
            collision.gameObject.transform.position = target.position;
            Camera.main.transform.position = fastTravelPosition;
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        panel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        panel.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
