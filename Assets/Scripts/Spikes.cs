using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 10;
    public Vector2 playerBack;
    public bool isBig;
    public GameObject blackScreen;
    private float blkTime;
    public Transform player;

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (player != null)
        {
            if (other.CompareTag("Player"))
            {

                other.GetComponent<Player>().DamagePlayer(10);
                other.transform.position = playerBack;

            }
        }
    }

}

