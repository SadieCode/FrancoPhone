using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word{
    public string Fr { get; set; }
    public string Eng { get; set; }
    public string AudioPath { get; set; }
    readonly AudioClip AudioFile;

    public Word()
    {
        AudioFile = (AudioClip) Resources.Load(AudioPath);
    }
}
