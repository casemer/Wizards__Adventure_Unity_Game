using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour
{

    void Start()
    {
        int lastScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastScene", lastScene);
    }

}
