using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLabels : MonoBehaviour {

    public Button btn;
    public Text txt;

    Text txtName;
    Text txtDescription;
    Text txtObjective;
    private Quest Quest;

    // Use this for initialization
    void Start()
    {
        txtName = GameObject.Find("TxtName").GetComponent<Text>();
        txtDescription = GameObject.Find("TxtDescription").GetComponent<Text>();
        txtObjective = GameObject.Find("TxtObjective").GetComponent<Text>();
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
        txtObjective.text = "Current Objective\n" + (Quest.Objectives[Quest.CurrentObjective]).Description;
    }
}
