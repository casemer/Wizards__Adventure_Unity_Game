using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public VectorValue playerStorage;
    public Vector2 playerPos;


    void OnTriggerEnted2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPos;
        }
    }
}
