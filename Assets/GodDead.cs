using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodDead : MonoBehaviour
{
    public GameObject god;
    public Collider2D col;

    void Start()
    {
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (god == null)
        {
            col.enabled = true;
        }


    }
}
