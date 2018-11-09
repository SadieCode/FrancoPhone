using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Noun : Word {
    [XmlElement("Gender")]
    public string Gender { get; set; }
    public string Plural { get; set; }

    public Noun() 
    {

    }

    public override void Display()
    {
        Text txtDetails = GameObject.Find("TxtDetails").GetComponent<Text>();
        String txt = Fr + " - " + Eng + "@";
        txt += Type + " (" + Gender + ")@@";
        txt += "Plural: " + Plural + "@@";
        txt += Ex + "@";
        txt += Trans + "@@";
        txtDetails.text = txt.Replace("@", "\n");
    }
}
