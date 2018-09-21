using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryLogic: MonoBehaviour{

    List<Word> PlayerDictionary = new List<Word>();
    List<Word> WordBank;
    public Transform DictionaryContent;
    public SimpleObjectPool WordBtnPool;

    private void Start()
    {
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
    }

    private void RemoveButtons()
    {
        while (DictionaryContent.childCount > 0)
        {
            GameObject toRemove = DictionaryContent.GetChild(0).gameObject;
            WordBtnPool.ReturnObject(toRemove);
        }
    }

    public void RefreshDisplay()
    {

    }
}
