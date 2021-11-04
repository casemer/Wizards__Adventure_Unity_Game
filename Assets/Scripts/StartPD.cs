using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartPD : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject crosshair;
    public GameObject footprint;
    public Collider2D throwArea;
    public Collider2D startPD;
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
            playerMove.enabled = false;
            crosshair.SetActive(false);
            throwArea.enabled = false;
            pauseMenu.enabled = false;
            StartCoroutine(Delay());

        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(7.0f);
        footprint.SetActive(true);
        //crosshair.SetActive(true);
        startPD.enabled = false;
        invisibleWall.enabled = true;
    }

}
