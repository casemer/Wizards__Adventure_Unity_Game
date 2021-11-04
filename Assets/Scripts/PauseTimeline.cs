using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseTimeline : MonoBehaviour
{

    public GameObject dialogueBox;
    public PlayableDirector pd;

    void Update()
    {
        if (dialogueBox.activeSelf)
        {
            pd.playableGraph.GetRootPlayable(0).SetSpeed(0);
        }
        if (!dialogueBox.activeSelf)
        {
            pd.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }
}
