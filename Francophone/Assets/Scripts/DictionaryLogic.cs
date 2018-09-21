using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DictionaryLogic: MonoBehaviour{

    List<Word> PlayerDictionary = new List<Word>();
    List<Word> WordBank;
    public Transform DictionaryContent;
    public SimpleObjectPool WordBtnPool;
    public GameObject DictionaryPanel;

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
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Word>), new XmlRootAttribute("Words"));
        TextReader textReader = new StreamReader(@"Assets/WordBank.xml");
        WordBank = (List<Word>)deserializer.Deserialize(textReader);
        textReader.Close();
        WordBank.Sort((x, y) => x.Fr.CompareTo(y.Fr));

        foreach (Word word in WordBank)
        {
            word.SetAudioFile();
        }

        //Add starting words to player's dictionary for testing purposes
        AddWord("Rouge");
        AddWord("Bleu");
        AddWord("Jaune");
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
        RemoveButtons();
        foreach (Word word in PlayerDictionary)
        {
            GameObject newWord = WordBtnPool.GetObject();
            newWord.transform.SetParent(DictionaryContent);
            DictionaryLabels dictionaryLabel = newWord.GetComponent<DictionaryLabels>();
            dictionaryLabel.Setup(word);
        }
        DictionaryPanel.gameObject.SetActive(true);
    }

    public void CloseDictionary()
    {
        DictionaryPanel.gameObject.SetActive(false);
    }

    private void RemoveButtons()
    {
        while (DictionaryContent.childCount > 0)
        {
            GameObject toRemove = DictionaryContent.GetChild(0).gameObject;
            WordBtnPool.ReturnObject(toRemove);
        }
    }

    /*For future use
    public void RefreshDisplay()
    {

    }*/
}
