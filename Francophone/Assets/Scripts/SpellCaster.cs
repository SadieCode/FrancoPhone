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
    readonly string[][] AnswerPool =
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
        string[] playerAnswerSplit = playerAnswer.Split(' ');

        for (int k = 0; k < Answers.Length; k++)
        {
            string correctAnswer = Answers[k];
            string[] correctAnswerSplit = correctAnswer.Split(' ');
            float tempPowerLevel = 0;

            for (int l = 0; l < playerAnswerSplit.Length; l++)
            {
                if (l > correctAnswerSplit.Length - 1)
                {
                    tempPowerLevel--;
                    if(tempPowerLevel < 0) { tempPowerLevel = 0; }
                    break;
                }
                
                if (correctAnswerSplit[l] == playerAnswerSplit[l]) {
                    tempPowerLevel++;
                }
            }

            if (tempPowerLevel != 0) { tempPowerLevel = (tempPowerLevel / correctAnswerSplit.Length) * 100; }
            if(tempPowerLevel >= powerLevel) {
                powerLevel = tempPowerLevel;
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
