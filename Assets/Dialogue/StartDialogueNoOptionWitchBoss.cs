using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartDialogueNoOptionWitchBoss : MonoBehaviour
{
    public DialogueTriggerNoOptionsWitchBoss start;
    bool playerInRange;
    public GameObject dialogueBox;
    public Collider2D talkSpot;
    public bool canTalkAgain = true;

    void Start()
    {
    }

    void Update()
    {
        if (!canTalkAgain)
        {
            if (playerInRange && PlayerPrefs.GetInt("TalkingWitchBoss") != 1)
            {
                talkSpot.enabled = false;
                PlayerPrefs.SetInt("TalkingWitchBoss", 1);
                start.TriggerDialogue();
            }
        }
        else
        {
            if (playerInRange)
            {
                talkSpot.enabled = false;
                start.TriggerDialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
