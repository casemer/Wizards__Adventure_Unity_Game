using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle centeredCam;
    public GameObject optionsMenu;
    public GameObject pauseMenu;



    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetCamera()
    {
        if (centeredCam.isOn)
        {
            PlayerPrefs.SetInt("CameraDistance", 0);
        }
        else
        {
            PlayerPrefs.SetInt("CameraDistance", 2);
        }
    }

    public void PauseMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
