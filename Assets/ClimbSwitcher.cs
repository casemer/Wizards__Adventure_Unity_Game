using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbSwitcher : MonoBehaviour
{
    PlayerController pc;
    ClimbController cc;
    Rigidbody2D rb;
    Animator anim;

    public ParticleSystem footSteps;
    private ParticleSystem.EmissionModule footEmission;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        cc = GetComponent<ClimbController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        footEmission = footSteps.emission;
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ClimbWall"))
        {
            pc.enabled = false;
            cc.enabled = true;
            anim.SetBool("IsClimbing", true);
            anim.SetFloat("Speed", 0);
            rb.gravityScale = 0.0f;
            footEmission.rateOverTime = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ClimbWall"))
        {
            pc.enabled = true;
            cc.enabled = false;
            anim.SetBool("IsClimbing", false);
            rb.gravityScale = 3.0f;
            rb.AddForce(transform.up * 20);

        }
    }
}
