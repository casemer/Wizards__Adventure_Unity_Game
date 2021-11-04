using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerForestNoOptions : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerForestNoOptions manager = FindObjectOfType<DialogueManagerForestNoOptions>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
