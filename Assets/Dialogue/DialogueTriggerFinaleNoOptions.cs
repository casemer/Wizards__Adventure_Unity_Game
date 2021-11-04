using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerFinaleNoOptions : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerFinaleNoOptions manager = FindObjectOfType<DialogueManagerFinaleNoOptions>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
