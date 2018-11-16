using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour {

    public DictionaryLogic dictionaryLogic;
    public GameObject dictionaryPanel;
    public InventoryLogic inventoryLogic;
    public GameObject inventoryPanel;
    public QuestLogic questLogic;
    public GameObject questPanel;
    public TimeLogic timeLogic;
    public GameObject calendarPanel;
    GameObject activePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open()
    {
        this.gameObject.SetActive(true);
        dictionaryLogic.OpenDictionary();
        activePanel = dictionaryPanel;
    }

    public void Close()
    {
        activePanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void DictionaryTab()
    {
        activePanel.SetActive(false);
        dictionaryLogic.OpenDictionary();
        activePanel = dictionaryPanel;
        activePanel.SetActive(true);
    }

    public void InventoryTab()
    {
        activePanel.SetActive(false);
        inventoryLogic.OpenInventory();
        activePanel = inventoryPanel;
        activePanel.SetActive(true);
    }

    public void QuestTab()
    {
        activePanel.SetActive(false);
        questLogic.OpenQuest();
        activePanel = questPanel;
        activePanel.SetActive(true);
    }

    public void CalendarTab()
    {
        activePanel.SetActive(false);
        timeLogic.OpenCalendar();
        activePanel = calendarPanel;
        activePanel.SetActive(true);
    }

    public void SettingsTab()
    {

    }


}
