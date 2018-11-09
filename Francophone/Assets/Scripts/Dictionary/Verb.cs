using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Verb : Word {
    public List<Conjugation> Conjugations { get; set; }

    public Verb()
    {

    }

    public override void Display()
    {
        Text txtDetails = GameObject.Find("TxtDetails").GetComponent<Text>();
        String txt = Fr + " - " + Eng + "@";
        txt += Type + "@@";
        txt += Ex + "@";
        txt += Trans + "@@";

        foreach (Conjugation c in Conjugations)
        {
            txt += "<b>" + c.Tense + "</b>@";
            txt += c.Je + "@";
            txt += c.Tu + "@";
            txt += c.Il + "@";
            txt += c.Nous + "@";
            txt += c.Vous + "@";
            txt += c.Ils + "@@";
        }

        txtDetails.text = txt.Replace("@", "\n");
    }
}
