using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour {

    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Slider volumeEffectsSlider;
    public Slider textSpeedSlider;
    public AudioSource audioSource;
    public Toggle toggleFont;

    //Game UI objects
    public GameObject movementUI;

    // Use this for initialization
    void Start () {
        settingsPanel.SetActive(false);

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        volumeEffectsSlider.value = PlayerPrefs.GetFloat("volumeEffects");
        textSpeedSlider.value = PlayerPrefs.GetFloat("textSpeed");
        
        /*if(PlayerPrefs.GetInt("largeFont") == 1)
        {
            toggleFont.isOn = true;
        }
        else
        {
            toggleFont.isOn = false;
        }*/
        
        //Screen.SetResolution(640, 480, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadGame()
    {

    }

    public void Practice()
    {

    }

    public void Info()
    {

    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
    }

    public void VolumeController()
    {
        audioSource.volume = volumeSlider.value;
        Save();
    }

    public void VolumeEffectsController()
    {
        PlayerPrefs.SetFloat("volumeEffects", volumeEffectsSlider.value);
    }

    public void TextSpeedController()
    {
        PlayerPrefs.SetFloat("textSpeed", textSpeedSlider.value);
    }

    public void FontController()
    {
        if (toggleFont.isOn)
        {
            PlayerPrefs.SetInt("largeFont", 1);
        }
        else
        {
            PlayerPrefs.SetInt("largeFont", 0);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
