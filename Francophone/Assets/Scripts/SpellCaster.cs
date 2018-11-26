using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCaster : MonoBehaviour{
    
    //Example sentences for testing
    public PlayerCombat player;
    string[] QuestionPool = {"Mardi vient après lundi.", "Mon anniversaire est le 17 mars.", "Noël tombe un vendredi.",
                            "Je suis née au mois d'octobre.","Mon père porte des chaussettes orange.","Les corneilles sont noires.",
                            "Les bananes sont jaunes.","Ma voiture est bleue."};
    string Question;
    string[][] AnswerPool =
    {
        new string[] {"Tuesday comes after Monday."},
        new string[] {"My birthday is March 17."},
        new string[] {"Christmas falls on a Friday."},
        new string[] {"I was born in the month of October."},
        new string[] {"My father wears orange socks."},
        new string[] {"Crows are black."},
        new string[] {"Bananas are yellow."},
        new string[] {"My car is blue."}
    };
    string[] Answers;
    string ClosestAnswer;
    string playerAnswer;
    int lastQuestion = -1;
    public static float powerLevel = 0;

    public InputField InputField;
    public GameObject SpellPanel;
    public Text TxtQuestion;
    public Text TxtPlayerAnswer;
    public Text TxtCorrectAnswer;
    public Text TxtPower;

    void Start()
    {
        InputField.onEndEdit.AddListener(CheckAnswer);
    }

    public void DisplayQuestion()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int index = UnityEngine.Random.Range(0, QuestionPool.Length);
        while (index == lastQuestion)
        {
            index = UnityEngine.Random.Range(0, QuestionPool.Length);
        }
        lastQuestion = index;
        Question = QuestionPool[index];
        Answers = AnswerPool[index];


        SpellPanel.SetActive(true);
        TxtQuestion.text = Question;
        TxtQuestion.gameObject.SetActive(true);
        InputField.text = "";
        InputField.gameObject.SetActive(true);
    }

    void CheckAnswer(string s)
    {
        powerLevel = 0;
        playerAnswer = s;
        List<string> attemptWords = new List<string>();
        string attemptWord = null;
        int i = 1;
        foreach (char c in playerAnswer)
        {
            if (i == playerAnswer.Length)
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

        for (int k = 0; k < Answers.Length; k++)
        {
            List<string> words = new List<string>();
            string word = null;
            int j = 1;
            string correctSentence = Answers[k];
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
                    wPowerLevel--;
                    if(wPowerLevel < 0) { wPowerLevel = 0; }
                    break;
                }
                
                if (correctArray[l] == attemptArray[l]) {
                    wPowerLevel++;
                }
            }

            if (wPowerLevel != 0) { wPowerLevel = (wPowerLevel / correctArray.Length) * 100; }
            if(wPowerLevel >= powerLevel) {
                powerLevel = wPowerLevel;
                ClosestAnswer = Answers[k];
            }
        }
        powerLevel = Mathf.Round(powerLevel);
        DisplayResult();
    }

    void DisplayResult()
    {
        TxtQuestion.gameObject.SetActive(false);
        InputField.gameObject.SetActive(false);
        TxtPlayerAnswer.gameObject.SetActive(true);
        TxtPlayerAnswer.text = "Your answer: " + playerAnswer;
        TxtCorrectAnswer.text = "Correct answer: " + ClosestAnswer;
        TxtCorrectAnswer.gameObject.SetActive(true);
        TxtPower.text = "Power: " + powerLevel + "%";
        TxtPower.gameObject.SetActive(true);
        Invoke("SpellComplete", 2.0f);
    }

    void SpellComplete()
    {
        TxtPlayerAnswer.gameObject.SetActive(false);
        TxtCorrectAnswer.gameObject.SetActive(false);
        TxtPower.gameObject.SetActive(false);
        SpellPanel.SetActive(false);
        CombatController.attackReady = true;
    }
}
