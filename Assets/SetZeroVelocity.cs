using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetZeroVelocity : MonoBehaviour
{
    public Rigidbody2D player;
    public Collider2D wall;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.velocity = Vector3.zero;
            StartCoroutine(DeleteWall());
        }
    }

    IEnumerator DeleteWall()
    {
        yield return new WaitForSeconds(0.5f);
        wall.enabled = false;
    }
}
