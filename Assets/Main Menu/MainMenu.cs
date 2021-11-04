using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public Vector2 playerPos;
    public VectorValue playerStorage;

    public Button loadButton;
    public Button beginButton;
    public Button newGameButton;

    public Vector2[] positions;

    void Start()
    {
        if (PlayerPrefs.GetInt("LastScene") > 0)
        {
            loadButton.gameObject.SetActive(true);
            newGameButton.gameObject.SetActive(true);
            beginButton.gameObject.SetActive(false);
        }
        else
        {
            loadButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
            beginButton.gameObject.SetActive(true);
        }
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerStorage.initialValue = playerPos;
        PlayerPrefs.SetInt("Health", 100);
        //the saves to be cleared
        PlayerPrefs.DeleteKey("LastScene");
        PlayerPrefs.DeleteKey("SkeletonBoss");
        PlayerPrefs.DeleteKey("TalkingIng");
        PlayerPrefs.DeleteKey("TalkingGuard");
        PlayerPrefs.DeleteKey("TalkingForest");
        PlayerPrefs.DeleteKey("TalkingAdventurer");
        PlayerPrefs.DeleteKey("TalkingAdventurer2");
        PlayerPrefs.DeleteKey("TalkingWitch");
        PlayerPrefs.DeleteKey("TalkingWitch1");
        PlayerPrefs.DeleteKey("TalkingWitchBoss");
        PlayerPrefs.DeleteKey("WitchBoss");
        PlayerPrefs.DeleteKey("Bjorgen");
        PlayerPrefs.DeleteKey("TalkingBjorgen");
        PlayerPrefs.DeleteKey("God");
        PlayerPrefs.DeleteKey("SecretIng");
        PlayerPrefs.DeleteKey("GodBoss");
        PlayerPrefs.DeleteKey("Megan 1");
        PlayerPrefs.DeleteKey("Megan 2");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        Application.Quit();
    }

    public void OptionsMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void LoadGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));

        if (PlayerPrefs.GetInt("Health") == 0)
        {
            PlayerPrefs.SetInt("Health", 100);
        }

        int level = PlayerPrefs.GetInt("LastScene");

        Scene scene = SceneManager.GetSceneByBuildIndex(level);

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

        playerStorage.initialValue = positions[level];


    }
}
