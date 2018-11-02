using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class LessonLogic : MonoBehaviour {

    public Text question;

    int numQuestions = 5;
    int questionsCompleted = 0;
    int correctAnswers = 0;
    int correctIndex = 0;
    int index = 0;
    int lastIndex = -1;
    bool correct = false;
    public bool readyForLesson = false;
    public static bool poorProgress = false;
    //answers are in 0 = french, 1 = english
    int language = 0;

    public GameObject quizPanel;
    public GameObject gradePanel;
    public Text[] optionButtons;
    public GameObject[] buttons;
    public Text txtGrade;
    string[] options = new string[4];
    public BoxCollider2D houseExit;

    readonly string[] english = { "January", "February","March","April","May","June","July",
        "August","September","October","November","December", "Sunday", "Monday",
        "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

    readonly string[] french = { "janvier", "février","mars","avril","mai","juin","juillet",
        "août","septembre","octobre","novembre","décembre", "dimanche", "lundi",
        "mardi", "mercredi", "jeudi", "vendredi", "samedi" };

    // Use this for initialization
    void Start () {

        NextQuestion();
	}

    [YarnCommand("SetReady")]
    public void SetReady()
    {
        Debug.Log("Hey");
        readyForLesson = true;
    }

    void NextQuestion()
    {
        foreach(GameObject btn in buttons)
        {
            btn.GetComponent<Button>().interactable = true;
        }

        index = Random.Range(0, english.Length);

        //don't repeat the last question
        while(index == lastIndex)
        {
            index = Random.Range(0, english.Length);
        }

        lastIndex = index;

        //0 = english, 1 = french
        language = Random.Range(0, 2);
        correctIndex = Random.Range(0, 4);

        if (language == 0)
        {
            question.text = "Translate: " + english[index];
            options[correctIndex] = french[index];
        }
        else
        {
            question.text = "Translate: " + french[index];
            options[correctIndex] = english[index];
        }

        int[] usedIndices = { -1, -1, -1, -1 };
        usedIndices[correctIndex] = index;

        for (int i = 0; i < 4; i++)
        {
            if (i == correctIndex) { }
            else
            {
                bool found = false;
                while (!found)
                {
                    found = true;
                    int j = Random.Range(0, english.Length);
                    foreach (int k in usedIndices)
                    {
                        if (j == k)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        usedIndices[i] = j;
                        if (language == 0) { options[i] = french[j]; }
                        else { options[i] = english[j]; }
                    }
                }
            }

        }

        for(int i = 0; i < 4; i++)
        {
            if (language == 0) { optionButtons[i].text = french[usedIndices[i]]; }
            else { optionButtons[i].text = english[usedIndices[i]]; }
        }
    }

    public void GradeQuestion(GameObject button)
    {
        if(language == 0){ 
            if(button.GetComponentInChildren<Text>().text == french[index])
            {
                correct = true;
            }
            else
            {
                correct = false;
            }
        }
        else
        {
            if (button.GetComponentInChildren<Text>().text == english[index])
            {
                correct = true;
            }
            else
            {
                correct = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            ColorBlock cb = buttons[i].GetComponent<Button>().colors;

            if (i == correctIndex)
            {
                cb.disabledColor = Color.green;
            }
            else
            {
                cb.disabledColor = Color.red;
            }

            buttons[i].GetComponent<Button>().colors = cb;
            buttons[i].GetComponent<Button>().interactable = false;
        }

        if (correct) { correctAnswers++; }
        questionsCompleted++;
        if (questionsCompleted < numQuestions)
        {
            Invoke("NextQuestion", 1.0f);
        }
        else
        {
            Invoke("GradeLesson", 1.0f);
        }
    }

    public void StartLesson()
    {
        quizPanel.SetActive(true);
        TimeLogic.stop = true;
    }

    void GradeLesson()
    {
        quizPanel.SetActive(false);
        gradePanel.SetActive(true);
        txtGrade.text = "Score: " + correctAnswers + "/" + numQuestions;

        //fix this into a float later
        if(correctAnswers <= 5)
        {
            poorProgress = true;
        }
    }

    public void FinishLesson()
    {
        TimeLogic.hour = 15;
        quizPanel.SetActive(false);
        gradePanel.SetActive(false);
        TimeLogic.stop = false;
        houseExit.isTrigger = true;
    }

    [YarnCommand("StartStudy")]
    public void StartStudy()
    {
        correctAnswers = 0;
        questionsCompleted = 0;
        lastIndex = -1;
        NextQuestion();
        StartLesson();
    }
}
