using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneTransition : MonoBehaviour {
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Animator anim;

    public AudioMixer audioMixer;

    private string exposedParam = "volume";
    private float volumeSet;

    void OnTriggerEnter2D(Collider2D other)
    {
        volumeSet = PlayerPrefs.GetFloat("gameVolume");
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            anim.SetTrigger("FadeOut");
            StartCoroutine(StartFade(audioMixer, exposedParam, 1f, volumeSet));
        }
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = playerPos;
        SceneManager.LoadScene(sceneToLoad);
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
