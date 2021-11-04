using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartPDWitch : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject crosshair;
    public GameObject footprint;
    public Collider2D startPD;
    public Transform t;

    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (t != null)
        {

            if (other.CompareTag("Player"))
            {
                pd.Play();
                crosshair.SetActive(false);
                footprint.SetActive(false);
                pauseMenu.enabled = false;
                StartCoroutine(Delay());
                t.Rotate(0f, 180f, 0f);
            }
        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(4.0f);
        footprint.SetActive(true);
        crosshair.SetActive(true);
        startPD.enabled = false;
    }

}
