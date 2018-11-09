using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
[XmlInclude(typeof(Noun))]
[XmlInclude(typeof(Adjective))]
[XmlInclude(typeof(Verb))]
public abstract class Word{
    public string Type { get; set; }
    public string Fr { get; set; }
    public string Eng { get; set; }
    public string Ex { get; set; }
    public string Trans { get; set; }

    protected Word()
    {

    }

    public virtual void Display() { }
}
