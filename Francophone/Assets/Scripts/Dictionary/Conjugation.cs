using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Conjugation
{
    public string Tense { get; set; }
    public string Je { get; set; }
    public string Tu { get; set; }
    public string Il { get; set; }
    public string Nous { get; set; }
    public string Vous { get; set; }
    public string Ils { get; set; }

    public Conjugation()
    {
    }
}
