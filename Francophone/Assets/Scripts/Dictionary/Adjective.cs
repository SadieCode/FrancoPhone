using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Adjective : Word {
    public string Masc { get; set; }
    public string Fem { get; set; }
    public string MascPl { get; set; }
    public string FemPl { get; set; }

    public Adjective()
    {

    }

    public override void Display()
    {
        Text txtDetails = GameObject.Find("TxtDetails").GetComponent<Text>();
        String txt = Fr + " - " + Eng + "@";
        txt += Type + "@@";
        txt += "Masculine:   " + Masc + "@";
        txt += "Masc Plural: " + MascPl + "@";
        txt += "Feminine:    " + Fem + "@";
        txt += "Fem Plural:  " + FemPl + "@@";
        txt += Ex + "@";
        txt += Trans + "@@";
        txtDetails.text = txt.Replace("@", "\n");
    }
}
