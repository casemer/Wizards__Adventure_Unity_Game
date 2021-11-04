using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneTransitionWithButton : MonoBehaviour {
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Animator anim;
    public AudioMixer audioMixer;
    private string exposedParam = "volume";
    private float volumeSet;

    void OnTriggerStay2D(Collider2D other)
    {
        volumeSet = PlayerPrefs.GetFloat("gameVolume");
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !other.isTrigger)
        {
            anim.SetTrigger("FadeOut");
            StartCoroutine(StartFade(audioMixer, exposedParam, 1f, volumeSet));
        }
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = playerPos;
        Scene scene = SceneManager.GetActiveScene();

        if ((PlayerPrefs.GetInt("SkeletonBoss") > 0) && (scene.name == "InsideBarn") || scene.name == "InsideHomePlayable")
        {
            if (PlayerPrefs.GetInt("WitchBoss") > 0)
            {
                SceneManager.LoadScene("TheTown3");
            }
            else
            {
                SceneManager.LoadScene("TheTown2");
            }
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float targetValue = volumeSet;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(targetValue, -50, currentTime / duration);
            audioMixer.SetFloat(exposedParam, newVol);
            yield return null;
        }
        yield break;
    }

}

