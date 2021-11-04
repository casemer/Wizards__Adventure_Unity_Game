using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartPDWitchBoss : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject crosshair;
    public Collider2D startPD;
    public GameObject footprint;
    public Collider2D invisibleWall;
    public PlayerController playerMove;

    private PauseMenu pauseMenu;

    void Start()
    {
        invisibleWall.enabled = false;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            pd.Play();
            footprint.SetActive(false);
            crosshair.SetActive(false);
            playerMove.enabled = false;
            pauseMenu.enabled = false;
            StartCoroutine(Delay());
            startPD.enabled = false;
        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        invisibleWall.enabled = true;
    }

}
