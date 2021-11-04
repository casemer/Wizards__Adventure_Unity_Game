using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDead : MonoBehaviour
{
    public GameObject witch, mage, mage2;
    public Collider2D col;

    void Start()
    {
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (witch == null && mage == null && mage2 == null)
        {
            col.enabled = true;
        }


    }
}
