using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        }
    }

    private void InitQuests()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Quest>), new XmlRootAttribute("Quests"));
        TextReader textReader = new StreamReader(@"Assets/Data/Quests.xml");
        Quests = (List<Quest>)deserializer.Deserialize(textReader);
        textReader.Close();
        Debug.Log(Quests[0].Status);
    }

    [YarnCommand("AddQuest")]
    public void AddQuest(string id)
    {
        Quest newQuest = Quests.Find(q => q.Id == int.Parse(id));
        newQuest.Status = 1;
        CurrentQuests.Add(newQuest);
    }

    /* For future use
    [YarnCommand("UpdareQuest")]
    public void UpdateQuest(string id)
    {
        Quest quest = CurrentQuests.Find(q => q.Id == int.Parse(id));
    }*/

    [YarnCommand("CompleteQuest")]
    public void CompleteQuest(string id)
    {
        Quest quest = CurrentQuests.Find(q => q.Id == int.Parse(id));
        quest.Status = 2;
    }

    public void OpenQuest()
    {
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
