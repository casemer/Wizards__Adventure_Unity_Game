using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringWaterTrigger : MonoBehaviour {
    public Dialogue[] conversations;

    public void TriggerDialogue()
    {
        SpringWaterEnd manager = FindObjectOfType<SpringWaterEnd>();
        manager.setIndex(0);
        manager.StartDialogue(conversations[0]);
    }
}
