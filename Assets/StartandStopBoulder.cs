using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartandStopBoulder : MonoBehaviour
{

    public bool start = true;
    public GameObject boulderTime;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(start)
            {
                boulderTime.SetActive(true);
            }
            else
            {
                boulderTime.SetActive(false);
            }
        }
    }

}
