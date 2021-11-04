using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNoOptionsGod : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerGodNoOptions manager = FindObjectOfType<DialogueManagerGodNoOptions>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
