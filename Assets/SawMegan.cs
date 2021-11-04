using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SawMegan : MonoBehaviour
{

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "InsideBarn")
        {
            PlayerPrefs.SetInt("Megan 1", 1);
        }
        if (scene.name == "InsideBarn2")
        {
            PlayerPrefs.SetInt("Megan 2", 1);
        }
    }
}
