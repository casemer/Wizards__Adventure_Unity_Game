using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SwitchMusic : MonoBehaviour
{
    public AudioMixer audioMixer;

    public string songBeingPlayed;
    public string songName;
    public float waitTime = 1f;
    public Animator bossHealthBar;
    private string exposedParam = "volume";
    private float volumeSet;
    private bool hasSwapped = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(hasSwapped);
        if (!hasSwapped)
        {
            volumeSet = PlayerPrefs.GetFloat("gameVolume");
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartFade(audioMixer, exposedParam, 1f, volumeSet));
                StartCoroutine(SmallWait());
                hasSwapped = true;
            }
            
        }
    }

    public IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(waitTime);
        FindObjectOfType<AudioManager>().Stop(songBeingPlayed);
        FindObjectOfType<AudioManager>().Play(songName);
        StartCoroutine(StartFadeIn(audioMixer, exposedParam, 1f, volumeSet));
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

    public IEnumerator StartFadeIn(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float targetValue = volumeSet;

        bossHealthBar.SetBool("dropDown", true);

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
