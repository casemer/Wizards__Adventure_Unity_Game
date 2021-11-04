using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBjorgen : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerBjorgen manager = FindObjectOfType<DialogueManagerBjorgen>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
