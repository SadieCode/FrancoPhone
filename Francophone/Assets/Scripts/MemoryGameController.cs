using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameController : MonoBehaviour {

    public Sprite bgImage;
    public Sprite[] options;

    public List<Sprite> gameOptions = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    public GameObject GamePanel;
    public GameObject GamePanelText;

    private bool firstSelected, secondSelected;
    private int firstSelectionIndex, secondSelectionIndex;
    private string firstSelectedCard, secondSelectedCard;

    private int numOfPairs;
    private int numOfMatches;

    public Text congrats;

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGameOptions();
        Shuffle(gameOptions);
        numOfPairs = gameOptions.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MemoryButton");

        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGameOptions()
    {
        int looper = btns.Count;

        for(int i = 0; i < looper; i++)
        {
            gameOptions.Add(options[i]);
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAnOption());         
        }
    }
    
    public void PickAnOption()
    {        
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstSelected)
        {            
            firstSelected = true;
            firstSelectionIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstSelectedCard = gameOptions[firstSelectionIndex].name;
            btns[firstSelectionIndex].image.sprite = gameOptions[firstSelectionIndex];
            
        }
        else if (!secondSelected && firstSelected == true)
        {
            secondSelected = true;
            secondSelectionIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondSelectedCard = gameOptions[secondSelectionIndex].name;
            btns[secondSelectionIndex].image.sprite = gameOptions[secondSelectionIndex];
        }

        if(firstSelected && secondSelected)
        {
            StartCoroutine(CheckIfTheSelectionsMatch());
        }
     
    }

    IEnumerator CheckIfTheSelectionsMatch()
    {    
        //yield return new WaitForSeconds(1f);
        if(firstSelectedCard == secondSelectedCard + "1" || secondSelectedCard == firstSelectedCard + "1")
        {
            yield return new WaitForSeconds(.5f);
            btns[firstSelectionIndex].interactable = false;
            btns[secondSelectionIndex].interactable = false;

            btns[firstSelectionIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondSelectionIndex].image.color = new Color(0, 0, 0, 0);

            numOfMatches++;
            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            btns[firstSelectionIndex].image.sprite = bgImage;
            btns[secondSelectionIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.5f);
        firstSelected = secondSelected = false;
    }

    void CheckIfTheGameIsFinished()
    {
        if(numOfMatches == numOfPairs)
        {
            StartCoroutine(GameOverTextAndAction());
        }
    }

    IEnumerator GameOverTextAndAction()
    {
        congrats.text = "Congratulations you got all the pairs!";
        yield return new WaitForSeconds(5f);

        GamePanel.SetActive(false);
        GamePanelText.SetActive(false);
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
