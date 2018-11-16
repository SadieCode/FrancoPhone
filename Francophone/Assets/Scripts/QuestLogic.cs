using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class QuestLogic : MonoBehaviour {

    public static List<Quest> CurrentQuests = new List<Quest>();
    public static List<Quest> Quests;
    public Transform QuestContent;
    public SimpleObjectPool QuestBtnPool;
    public GameObject QuestPanel;
    public GameObject MovementUI;
    public GameObject btnPrefab;
    public GameObject DetailsPanel;

    // Use this for initialization
    void Start () {
        //hide quest panel as default state
        QuestPanel.gameObject.SetActive(false);

        //for future when save file is enabled
        if (Quests == null)
        {
            InitQuests();
            AddQuest("0");
        }
    }

    private void InitQuests()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/Quests", typeof(TextAsset));
        StringReader stringReader = new StringReader(textAsset.text);
        XmlTextReader reader = new XmlTextReader(stringReader);

        XmlSerializer serial = new XmlSerializer(typeof(List<Quest>), new XmlRootAttribute("Quests"));
        Quests = (List<Quest>)serial.Deserialize(reader);
        stringReader.Close();
        reader.Close();
    }

    [YarnCommand("AddQuest")]
    public void AddQuest(string id)
    {
        Quest newQuest = Quests.Find(q => q.Id == int.Parse(id));
        newQuest.Status = 1;
        CurrentQuests.Add(newQuest);
    }

    [YarnCommand("NextObjective")]
    public void NextObjective(string id)
    {
        Quest quest = Quests.Find(q => q.Id == int.Parse(id));
        quest.NextObjective();
    }

    public void OpenQuest()
    {
        if (QuestPanel.activeSelf) { return; }
        MovementUI.SetActive(false);
        RemoveButtons();
        foreach (Text text in DetailsPanel.GetComponentsInChildren<Text>())
        {
            text.text = null;
        }
        foreach (Quest quest in CurrentQuests)
        {
            if (quest.Status == 1)
            {
                GameObject newQuest = QuestBtnPool.GetObject();
                newQuest.transform.SetParent(QuestContent);
                QuestLabels questLabel = newQuest.GetComponent<QuestLabels>();
                questLabel.Setup(quest);
                newQuest.GetComponent<RectTransform>().localScale = btnPrefab.GetComponent<RectTransform>().localScale;
            }
        }
        QuestPanel.gameObject.SetActive(true);
    }

    public void CloseQuest()
    {
        QuestPanel.gameObject.SetActive(false);
        MovementUI.SetActive(true);
    }

    private void RemoveButtons()
    {
        while (QuestContent.childCount > 0)
        {
            GameObject toRemove = QuestContent.GetChild(0).gameObject;
            QuestBtnPool.ReturnObject(toRemove);
        }
    }
}
