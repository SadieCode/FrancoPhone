using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryLabels : MonoBehaviour {

    public Button btn;
    public Text txt;
    AudioSource AudioSource;

    private Word Word;

	// Use this for initialization
	void Start () {
        AudioSource = GameObject.Find("GENERAL").GetComponent<AudioSource>();
        btn.onClick.AddListener(HandlePress);
    }

    public void Setup(Word currentWord)
    {
        Word = currentWord;
        txt.text = Word.Fr + "/" + Word.Eng;
    }
	
    public void HandlePress()
    {
        AudioSource.PlayOneShot(Word.AudioFile);
    }
}
