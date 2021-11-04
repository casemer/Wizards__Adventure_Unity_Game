using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAdventurerDialogue : MonoBehaviour
{
    public DialogueTrigger start;
    bool playerInRange;
    public GameObject dialogueBox;
    public GameObject bubble;
    public Collider2D talkSpot;
    public bool canTalkAgain = true;

    void Start()
    {
        bubble.SetActive(false);
    }

    void Update()
    {
        if (!canTalkAgain)
        {
            if (Input.GetButtonDown("Interact") && playerInRange && PlayerPrefs.GetInt("TalkingOptionAdventurer") != 1)
            {
                dialogueBox.SetActive(true);
                talkSpot.enabled = false;
                Destroy(bubble);
                PlayerPrefs.SetInt("TalkingOptionAdventurer", 1);
                start.TriggerDialogue();
            }

            if (PlayerPrefs.GetInt("TalkingOptionAdventurer") == 1)
            {
                talkSpot.enabled = false;
                Destroy(bubble);
            }
        }
        else
        {
            if (Input.GetButtonDown("Interact") && playerInRange)
            {
                dialogueBox.SetActive(true);
                talkSpot.enabled = false;
                Destroy(bubble);
                PlayerPrefs.SetInt("TalkingOptionAdventurer", 1);
                start.TriggerDialogue();
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            bubble.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bubble.SetActive(false);
            playerInRange = false;
        }
    }
}
