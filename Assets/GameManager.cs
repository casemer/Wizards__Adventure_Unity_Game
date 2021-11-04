
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject gameOverUI;

    public static bool gameHasEnded;

    public Player player;
    public GameObject pauseMenuUI;
    public PauseMenu pauseMenu;
    public GameObject crosshair;
    public AudioMixer audioMixer;
    private string exposedParam = "volume";
    private float volumeSet;

    void Start()
    {
        volumeSet = PlayerPrefs.GetFloat("gameVolume");
        audioMixer.SetFloat(exposedParam, -80);

        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();

        }

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "InsideTheHome" || scene.name == "InsideTheHomePlayable" || scene.name == "InsideBarn" || scene.name == "InsideBarn2" || scene.name == "Finale")
        {
            FindObjectOfType<AudioManager>().Play("HomeMusic");
        }

        else if (scene.name == "TheTown" || scene.name == "TheTown2" || scene.name == "TheTown3")
        {
            FindObjectOfType<AudioManager>().Play("TownMusic");
        }

        else if (scene.name == "Dungeon" || scene.name == "ThroneRoom")
        {
            FindObjectOfType<AudioManager>().Play("DungeonMusic");
        }

        else if (scene.name == "Forest" || scene.name == "ForestPond")
        {
            FindObjectOfType<AudioManager>().Play("ForestMusic");
        }

        else if (scene.name == "Mountains")
        {
            FindObjectOfType<AudioManager>().Play("MountainMusic");
        }
        else if (scene.name == "Heaven")
        {
            FindObjectOfType<AudioManager>().Play("HeavenMusic");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Music");
        }
        StartCoroutine(StartFadeIn(audioMixer, exposedParam, 1f, volumeSet));


        gameHasEnded = false;
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }

    public IEnumerator StartFadeIn(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float targetValue = volumeSet;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(-50, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, newVol);
            yield return null;
        }
        yield break;
    }


}
