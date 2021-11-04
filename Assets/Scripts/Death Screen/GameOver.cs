using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Scene scene;

    void Start()
    {
        Cursor.visible = true;
    }

    public void StartOver()
    {
        if (PlayerPrefs.GetInt("Hardcore") > 0)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("InsideTheHome");
        }

        else
        {
            Scene scene = SceneManager.GetActiveScene();

            FindObjectOfType<AudioManager>().Play("Button");
            Cursor.visible = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (PlayerPrefs.GetInt("Hardcore") > 0)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                PlayerPrefs.SetInt("Health", 100);
            }

            if (scene.name == "Forest")
            {
                PlayerPrefs.DeleteKey("TalkingWitch");
                PlayerPrefs.DeleteKey("TalkingWitch1");
            }

            if (scene.name == "ForestPond")
            {
                PlayerPrefs.DeleteKey("TalkingWitchBoss");
            }
            if (scene.name == "Heaven")
            {
                PlayerPrefs.DeleteKey("God");
            }
        }
        PlayerPrefs.SetInt("Health", 100);
    }

    public void Menu()
    {
        if (PlayerPrefs.GetInt("Hardcore") > 0)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
        }

            SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("Button");


    }

}
