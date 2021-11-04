using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartPDWitchOne : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject crosshair;
    public GameObject footprint;
    public Collider2D startPD;
    public Rigidbody2D player;

    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {

            if (other.CompareTag("Player"))
            {
                pd.Play();
                footprint.SetActive(false);
                crosshair.SetActive(false);
                pauseMenu.enabled = false;
                StartCoroutine(Delay());
                player.velocity = Vector3.zero;
            }
        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(7.0f);
        footprint.SetActive(true);
        crosshair.SetActive(true);
        startPD.enabled = false;
    }

}
