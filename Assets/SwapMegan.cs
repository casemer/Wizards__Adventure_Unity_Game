using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMegan : MonoBehaviour
{
    public GameObject guard;
    public GameObject megan;


    void Start()
    {
        if ((PlayerPrefs.GetInt("Megan 1", 0) == 1) && (PlayerPrefs.GetInt("Megan 2", 0) == 1))
        {
            guard.SetActive(false);
            megan.SetActive(true);
        }


    }

}
