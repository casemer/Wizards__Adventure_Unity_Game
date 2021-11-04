using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopMovementWitch : MonoBehaviour {
    public GameObject playerMove;
    public DialogueManagerNoOptionsWitchBoss dialogue;
    public SpringWaterEnd spring;
    public PlayableDirector pd;
    public GameObject footDust;
    public Crosshair crosshair;
    public GameObject pauseMenu;
    public GameObject crosshairs;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (playerMove != null)
        {

            if (pd.state == PlayState.Playing || dialogue.isTalking || spring.isTalking)
            {
                playerMove.GetComponent<PlayerController>().enabled = false;
                crosshair.enabled = false;
                footDust.SetActive(false);
            }
            else if (!dialogue.isTalking && !pauseMenu.activeSelf && !spring.isTalking)
            {
                playerMove.GetComponent<PlayerController>().enabled = true;
                footDust.SetActive(true);
                crosshair.enabled = true;
                crosshairs.SetActive(true);
            }
        }
    }

}
