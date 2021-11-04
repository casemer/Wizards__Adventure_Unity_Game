using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRootTrigger : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        DarkRootEnd manager = FindObjectOfType<DarkRootEnd>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
