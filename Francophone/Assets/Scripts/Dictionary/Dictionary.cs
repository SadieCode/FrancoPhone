using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
[XmlType]
public class Dictionary{
    [XmlArray("Words")]
    [XmlArrayItem("Noun", typeof(Noun)), XmlArrayItem("Adjective", typeof(Adjective)),XmlArrayItem("Verb", typeof(Verb))]
    public List<Word> Words { get; set; }

    public Dictionary() { }
}
