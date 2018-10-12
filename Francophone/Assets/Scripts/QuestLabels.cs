using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLabels : MonoBehaviour {

    public Button btn;
    public Text txt;

    Text txtName;
    Text txtDescription;
    private Quest Quest;

    // Use this for initialization
    void Start()
    {
        txtName = GameObject.Find("TxtName").GetComponent<Text>();
        txtDescription = GameObject.Find("TxtDescription").GetComponent<Text>();
        btn.onClick.AddListener(HandlePress);
    }

    public void Setup(Quest currentQuest)
    {
        Quest = currentQuest;
        txt.text = Quest.Name;
    }

    public void HandlePress()
    {

        txtName.text = Quest.Name;
        txtDescription.text = Quest.Description;
    }
}
