using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DictionaryLogic: MonoBehaviour{

    public static List<Word> PlayerDictionary = new List<Word>();
    public static List<Word> WordBank;
    public Transform DictionaryContent;
    public SimpleObjectPool WordBtnPool;
    public GameObject DictionaryPanel;
    public GameObject MovementUI;
    public GameObject btnPrefab;
    public GameObject btnDictionary;
    public GameObject wordPanel;

    private void Start()
    {
        //hide dictionary as default state
        DictionaryPanel.gameObject.SetActive(false);

        //for future when save file is enabled
        if (WordBank == null)
        {
            InitCompleteDictionary();
        }
    }

    private void InitCompleteDictionary()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/WordBank", typeof(TextAsset));
        StringReader stringReader = new StringReader(textAsset.text);
        XmlTextReader reader = new XmlTextReader(stringReader);

        XmlSerializer serial = new XmlSerializer(typeof(List<Word>), new XmlRootAttribute("Words"));
        WordBank = (List<Word>)serial.Deserialize(reader);
        stringReader.Close();
        reader.Close();

        WordBank.Sort((x, y) => x.Fr.CompareTo(y.Fr));

        //Add starting words to player's dictionary for testing purposes
        AddWord("Rouge");
        AddWord("Bleu");
    }

    [YarnCommand("AddWord")]
    public void AddWord(string fr)
    {
        Word newWord = WordBank.Find(w => w.Fr == fr);
        PlayerDictionary.Add(newWord);
        SortFr();
    }

    public void SortFr()
    {
        PlayerDictionary.Sort((x, y) => x.Fr.CompareTo(y.Fr));
    }

    public void SortEng()
    {
        PlayerDictionary.Sort((x, y) => x.Eng.CompareTo(y.Eng));
    }

    public void OpenDictionary()
    {
        MovementUI.SetActive(false);
        RemoveButtons();
        foreach (Word word in PlayerDictionary)
        {
            GameObject newWord = WordBtnPool.GetObject();
            newWord.transform.SetParent(DictionaryContent);
            DictionaryLabels dictionaryLabel = newWord.GetComponent<DictionaryLabels>();
            dictionaryLabel.Setup(word);
            newWord.GetComponent<RectTransform>().localScale = btnPrefab.GetComponent<RectTransform>().localScale;
        }

        foreach (Text text in wordPanel.GetComponentsInChildren<Text>())
        {
            text.text = null;
        }

        DictionaryPanel.gameObject.SetActive(true);
    }

    public void CloseDictionary()
    {
        DictionaryPanel.gameObject.SetActive(false);
        MovementUI.SetActive(true);
    }

    private void RemoveButtons()
    {
        while (DictionaryContent.childCount > 0)
        {
            GameObject toRemove = DictionaryContent.GetChild(0).gameObject;
            WordBtnPool.ReturnObject(toRemove);
        }
    }

    [YarnCommand("EquipFrancophone")]
    public void EquipFrancophone()
    {
        btnDictionary.SetActive(true);
    }

    /*For future use
    public void RefreshDisplay()
    {

    }*/
}
