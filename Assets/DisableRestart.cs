using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableRestart : MonoBehaviour
{
    public Button restartButton;
    public Image skullCover;

    void Start()
    {
        if (PlayerPrefs.GetInt("Hardcore") > 0)
        {
            restartButton.interactable = false;
            skullCover.enabled = true;
        }
        else
        {
            skullCover.enabled = false;
            restartButton.interactable = true;
        }
    }

}
