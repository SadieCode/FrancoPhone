using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
[XmlRoot(ElementName = "Objectives\\Objective")]
public class Objective {
    [XmlElement(ElementName = "Description")]
    public string Description { get; set; }

    Objective() { }
}
