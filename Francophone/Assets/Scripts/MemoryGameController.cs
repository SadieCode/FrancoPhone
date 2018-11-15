using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameController : MonoBehaviour {

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] options;

    public List<Sprite> gameOptions = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    private bool firstSelection, secondSelection;
    private int firstSelectionIndex, secondSelectionIndex;
    private string firstSelectionOption, secondSelectionOption;


    private int countCorrectGuesses;
    private int gameGuesses;

    void Awake()
    {
       // options = Resources.
    }
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGameOptions();
        Shuffle(gameOptions);
        gameGuesses = gameOptions.Count / 2;
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
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }

            gameOptions.Add(options[index]);

            index++;
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

        if (!firstSelection)
        {
            firstSelection = true;
            firstSelectionIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstSelectionOption = gameOptions[firstSelectionIndex].name;
            btns[firstSelectionIndex].image.sprite = gameOptions[firstSelectionIndex];
        }
        else if (!secondSelection)
        {
            secondSelection = true;
            secondSelectionIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondSelectionOption = gameOptions[secondSelectionIndex].name;
            btns[secondSelectionIndex].image.sprite = gameOptions[secondSelectionIndex];
        }

        StartCoroutine(CheckIfTheSelectionsMatch());
    }

    IEnumerator CheckIfTheSelectionsMatch()
    {
        yield return new WaitForSeconds(1f);

        if(firstSelectionOption == secondSelectionOption)
        {
            yield return new WaitForSeconds(1f);
            btns[firstSelectionIndex].interactable = false;
            btns[secondSelectionIndex].interactable = false;

            btns[firstSelectionIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondSelectionIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            btns[firstSelectionIndex].image.sprite = bgImage;
            btns[secondSelectionIndex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(.5f);
        firstSelection = secondSelection = false;

    }

    void CheckIfTheGameIsFinished()
    {
        if(countCorrectGuesses == gameGuesses)
        {

        }
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
