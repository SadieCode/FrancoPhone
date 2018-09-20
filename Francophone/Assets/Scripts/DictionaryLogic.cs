using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryLogic: MonoBehaviour{

    List<Word> PlayerDictionary = new List<Word>();
    List<Word> WordBank;
    public GameObject ContentArea;
    public GameObject Label;

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

        //Add starting words to player's dictionary for testing purposes
        AddWord("Rouge");
        AddWord("Bleu");
        AddWord("Jaune");
    }

    public void AddWord(string fr)
    {
        Word newWord = WordBank.Find(w => w.Fr == fr);
        PlayerDictionary.Add(newWord);
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
        foreach(Word word in PlayerDictionary)
        {
            GameObject label = Instantiate(Label);
            var button = label.GetComponent<UnityEngine.UI.Button>();
            button.GetComponentInChildren<Text>().text = word.Fr;
            button.transform.SetParent(ContentArea.transform);
        }
    }
}
