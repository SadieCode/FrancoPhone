using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    AudioSource source;
	// Use this for initialization
	void Start () {
		source = GameObject.Find("GENERAL").GetComponent<AudioSource>();
    }

    public void Play()
    {
        source.PlayOneShot(source.clip);
    }
}
