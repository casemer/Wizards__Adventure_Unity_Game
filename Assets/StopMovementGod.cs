using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopMovementGod : MonoBehaviour {
    public GameObject playerMove;
    public DialogueManagerGodNoOptions dialogue;
    public BrothEnd broth;
    public PlayableDirector pd, pd2;
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
            if (pd.state == PlayState.Playing || pd2.state == PlayState.Playing || dialogue.isTalking || broth.isTalking)
            {
                playerMove.GetComponent<PlayerController>().enabled = false;
                crosshair.enabled = false;
                footDust.SetActive(false);
            }
            else if (!dialogue.isTalking && !pauseMenu.activeSelf && !broth.isTalking)
            {
                playerMove.GetComponent<PlayerController>().enabled = true;
                footDust.SetActive(true);
                crosshair.enabled = true;
                crosshairs.SetActive(true);
            }
        }
    }

}
