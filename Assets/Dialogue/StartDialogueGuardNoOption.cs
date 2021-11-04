using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartDialogueGuardNoOption : MonoBehaviour
{
    public DialogueTriggerNoOptions start;
    bool playerInRange;
    public GameObject dialogueBox;
    public Collider2D talkSpot;
    public bool canTalkAgain = true;
    public Rigidbody2D player;

    void Start()
    {
    }

    void Update()
    {
        if (!canTalkAgain)
        {
            if (playerInRange && PlayerPrefs.GetInt("TalkingGuard") != 1)
            {
                talkSpot.enabled = false;
                PlayerPrefs.SetInt("TalkingGuard", 1);
                start.TriggerDialogue();
                player.velocity = Vector3.zero;
            }
        }
        else
        {
            if (playerInRange)
            {
                talkSpot.enabled = false;
                start.TriggerDialogue();
                player.velocity = Vector3.zero;
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
