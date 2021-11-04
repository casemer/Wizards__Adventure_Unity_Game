using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrothTrigger : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        BrothEnd manager = FindObjectOfType<BrothEnd>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
