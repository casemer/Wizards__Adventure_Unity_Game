using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public bool pausedGame = false;
    AudioSource Audio;
    public GameObject pauseMenuUI;

    public GameObject crosshair;

    public GameObject gameOverScreen;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame)
            {
                Resume();
            }
            else if (!gameOverScreen.activeSelf)
            {
                Pause();
            }

        }

    }


    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pausedGame = false;
        Cursor.visible = false;
        crosshair.GetComponent<Crosshair>().enabled = true;
    }

    void Pause()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        crosshair.GetComponent<Crosshair>().enabled = false;
        pausedGame = true;
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().Stop("Music");
    }

    public void QuitGame()
    {
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("Button");
    }

    public void StartOver()
    {
        Cursor.visible = false;

        Scene scene = SceneManager.GetActiveScene();

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
            PlayerPrefs.DeleteKey("SecretIng");
        }

        if (scene.name == "TheTown")
        {
            PlayerPrefs.DeleteKey("TalkingForest");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("Button");


    }

}
