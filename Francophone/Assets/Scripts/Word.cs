using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Word{
    public string Fr { get; set; }
    public string Eng { get; set; }
    public string Inf { get; set; }
    public string Ex { get; set; }
    public string Trans { get; set; }
    public string AudioPath { get; set; }
    //public AudioClip AudioFile;

    public Word()
    {

    }

    public void SetAudioFile()
    {
        //AudioFile = Resources.Load<AudioClip>(AudioPath);
    }
}
