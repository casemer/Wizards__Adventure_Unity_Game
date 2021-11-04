using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNoOptionsWitchBoss : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DialogueManagerNoOptionsWitchBoss manager = FindObjectOfType<DialogueManagerNoOptionsWitchBoss>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
