using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportSound : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (other.CompareTag("Player"))
        {
            if (scene.name == "TheTown3")
            {
                FindObjectOfType<AudioManager>().Play("TeleportIn");
            }
            else if (scene.name == "Mountains")
            {
                FindObjectOfType<AudioManager>().Play("TeleportOut");
            }
        }
    }
}

