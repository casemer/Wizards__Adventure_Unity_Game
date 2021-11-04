using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SwitchOnlyMusic : MonoBehaviour {

    public AudioMixer audioMixer;
    public string songBeingPlayed;
    public string songName;
    public float waitTime = 1f;
    public Animator bossHealthBar;
    private bool hasSwapped = false;
    private AudioSource audioSource;
    private AudioSource nextAudioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        audioSource = FindObjectOfType<AudioManager>().GetName(songBeingPlayed);
        nextAudioSource = FindObjectOfType<AudioManager>().GetName(songName);
        if (!hasSwapped)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartFade(audioSource, 1f, 0.0f));
                StartCoroutine(SmallWait());
                hasSwapped = true;
            }

        }
    }

    public IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(StartFadeIn(nextAudioSource, 1f, 0.1f));
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        FindObjectOfType<AudioManager>().Stop(songBeingPlayed);
        yield break;
    }

    public IEnumerator StartFadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = 0;
        FindObjectOfType<AudioManager>().Play(songName);
        bossHealthBar.SetBool("dropDown", true);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
