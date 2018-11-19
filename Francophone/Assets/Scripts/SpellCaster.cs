using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCaster : MonoBehaviour{
    
    //Example sentences for testing
    public InputField inputField;
    string sentence = "Je ne veux pas";
    string[] answers = { "I don't want to", "I do not want to" };
    string answer;
    string attempt;
    float powerLevel = 0;

    public GameObject DisplayPanel;
    public Text questionText;
    public Text correctText;
    public Text powerText;

    void Start()
    {
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    public void Attack()
    {
        DisplayPanel.SetActive(true);
        questionText.text = sentence;
    }

    void CheckAnswer(string s)
    {
        powerLevel = 0;
        attempt = s;
        List<string> attemptWords = new List<string>();
        string attemptWord = null;
        int i = 1;
        foreach (char c in attempt)
        {
            if (i == attempt.Length)
            {
                attemptWord = attemptWord + c;
                attemptWords.Add(attemptWord);
                attemptWord = null;
            }
            else if (c != ' ')
            {
                attemptWord = attemptWord + c;
            }
            else
            {
                attemptWords.Add(attemptWord);
                attemptWord = null;
            }
            i++;
        }

        for (int k = 0; k < answers.Length; k++)
        {
            List<string> words = new List<string>();
            string word = null;
            int j = 1;
            string correctSentence = answers[k];
            foreach (char c in correctSentence)
            {
                if (j == correctSentence.Length)
                {
                    word = word + c;
                    words.Add(word);
                    word = null;
                }
                else if (c != ' ')
                {
                    word = word + c;
                }
                else
                {
                    words.Add(word);
                    word = null;
                }
                j++;
            }

            string[] attemptArray = attemptWords.ToArray();
            string[] correctArray = words.ToArray();

            float wPowerLevel = 0;
            for (int l = 0; l < attemptArray.Length; l++)
            {
                if (l > correctArray.Length - 1)
                {
                    break;
                }
                
                if (correctArray[l] == attemptArray[l]) {
                    wPowerLevel++;
                }
            }

            if (wPowerLevel != 0) { wPowerLevel = (wPowerLevel / correctArray.Length) * 100; }
            if(wPowerLevel >= powerLevel) {
                powerLevel = wPowerLevel;
                answer = answers[k];
            }
        }
        DisplayResult();
    }

    void DisplayResult()
    {
        correctText.text = answer;
        correctText.gameObject.SetActive(true);
        powerText.text = "Power: " + powerLevel;
        powerText.gameObject.SetActive(true);
    }
}
