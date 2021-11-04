using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject videoMenu;
    public Toggle hardcoreToggle;
    public GameObject hardcoreWarning;
    public Slider slider;
    private bool hasCheckedHardcore;



    void Start() {

        if(PlayerPrefs.GetInt("Hardcore") > 0) 
        {
            hardcoreToggle.isOn = true;
        }
        else
        {
            hardcoreToggle.isOn = false;
        }
        slider.value = PlayerPrefs.GetFloat("gameVolume");

    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("gameVolume", volume);
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }



    public void VideoMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(false);
        videoMenu.SetActive(true);
    }

    public void HardcoreOn(bool isHardcore)
    {
        if (hardcoreToggle.isOn)
        {
            PlayerPrefs.SetInt("Hardcore", 1);

            if (mainMenu.activeSelf)
            {
                hardcoreWarning.SetActive(false);
            }
            if (optionsMenu.activeSelf)
            {
                hardcoreWarning.SetActive(true);
            }

        }
        else
        {
            PlayerPrefs.SetInt("Hardcore", 0);
        }
    }


}
