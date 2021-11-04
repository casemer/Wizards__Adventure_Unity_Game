using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSwordAway : MonoBehaviour
{
    public Animator anim;
    public float timeToPutAway = 5f;
    public GameObject footDust;

   void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            footDust.SetActive(false);
            StartCoroutine(PutAway());
        }
    }

    IEnumerator PutAway()
    {
        yield return new WaitForSeconds(timeToPutAway);
        anim.SetBool("sword", true);
        footDust.SetActive(true);
    }
}
