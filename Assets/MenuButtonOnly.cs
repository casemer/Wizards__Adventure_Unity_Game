using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonOnly : MonoBehaviour
{

    public void LoadMenu()
    {
        PlayerPrefs.DeleteAll();
        FindObjectOfType<AudioManager>().Play("Button");
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().Stop("Music");
    }
}
