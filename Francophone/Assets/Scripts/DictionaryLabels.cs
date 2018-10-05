using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryLabels : MonoBehaviour {

    public Button btn;
    public Text txt;
    //AudioSource AudioSource;

    Text txtFr;
    Text txtEng;
    Text txtInf;
    Text txtEx;
    Text txtTrans;
    private Word Word;

	// Use this for initialization
	void Start () {
        //AudioSource = GameObject.Find("GENERAL").GetComponent<AudioSource>();
        txtFr = GameObject.Find("TxtFr").GetComponent<Text>();
        txtEng = GameObject.Find("TxtEng").GetComponent<Text>();
        txtInf = GameObject.Find("TxtInf").GetComponent<Text>();
        txtEx = GameObject.Find("TxtEx").GetComponent<Text>();
        txtTrans = GameObject.Find("TxtTrans").GetComponent<Text>();
        btn.onClick.AddListener(HandlePress);
    }

    public void Setup(Word currentWord)
    {
        Word = currentWord;
        txt.text = Word.Fr + " - " + Word.Eng;
    }
	
    public void HandlePress()
    {

        txtFr.text = Word.Fr;
        txtEng.text = Word.Eng;
        txtInf.text = Word.Inf.Replace("@", "\n");
        txtEx.text = Word.Ex;
        txtTrans.text = Word.Trans;
    }
}