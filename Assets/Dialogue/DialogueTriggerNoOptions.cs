using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNoOptions : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerNoOptions manager = FindObjectOfType<DialogueManagerNoOptions>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
