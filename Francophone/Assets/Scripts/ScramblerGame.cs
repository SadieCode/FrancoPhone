using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScramblerGame : MonoBehaviour {
    string scrambledSentence;
    string correctSentence = "Je ne veux pas";
    bool correct = false;

    public InputField inputField;
    public Text display;
    public Image indicator;

	// Use this for initialization
	void Start () {
        inputField.onEndEdit.AddListener(CheckAnswer);
        List<string> words = new List<string>();
        string word = null;
        int i = 1;
        foreach(char c in correctSentence)
        {
            if (i == correctSentence.Length)
            {
                word = word + c;
                words.Add(word);
                word = null;
            }
            else if(c != ' ')
            {
                word = word + c;
            }
            else
            {
                words.Add(word);
                word = null;
            }
            i++;
        }

        words.ShuffleList();

        foreach (string s in words)
        {
            scrambledSentence = scrambledSentence + " " + s;
        }

        display.text = scrambledSentence;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckAnswer(string answer)
    {
        if(answer.ToLower() == correctSentence.ToLower())
        {
            correct = true;
            indicator.color = Color.green;
        }
        else
        {
            correct = false;
            indicator.color = Color.red;
        }
    }
}
