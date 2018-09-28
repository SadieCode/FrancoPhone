using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public static GameObject startPanel;
    public GameObject uiPanel;
    public Button button;
    // Use this for initialization
    void Start () {
        startPanel = GameObject.Find("MainMenu");
        //uiPanel = GameObject.Find("UI");


        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(onclickStart);
    }

    public void onclickStart()
    {
        startPanel.SetActive(false);
        uiPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
