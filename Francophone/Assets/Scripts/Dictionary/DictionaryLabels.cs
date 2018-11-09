using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryLabels : MonoBehaviour {

    public Button btn;
    public Text txt;
    AudioSource audioSource;

    public AudioClip audioClip;
    private Word Word;

	// Use this for initialization
	void Start () {
        audioSource = GameObject.Find("GENERAL").GetComponent<AudioSource>();
        btn.onClick.AddListener(HandlePress);
    }

    public void Setup(Word currentWord)
    {
        Word = currentWord;
        txt.text = Word.Fr + " - " + Word.Eng;
        this.gameObject.name = Word.Fr;
    }
	
    public void HandlePress()
    {
        audioClip = Resources.Load<AudioClip>("Audio/" + Word.Fr.ToLower());
        audioSource.clip = audioClip;
        Word.Display();
    }
}