using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyFrog : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public float thrust = 20;
    public Transform target;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (target != null)
        {

            Vector3 toTarget = (target.position - transform.position).normalized;

            if (other.CompareTag("Player"))
            {
                if (Vector3.Dot(toTarget, transform.right) > 0)
                {
                    rb.AddForce(transform.up * thrust);
                    rb.AddForce(transform.right * -thrust);
                    transform.Rotate(0f, 180f, 0f);
                }
                else
                {
                    rb.AddForce(transform.up * thrust);
                    rb.AddForce(transform.right * thrust);
                }

                anim.SetBool("isJumping", true);
                StartCoroutine(SmallWait());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (target != null)
        {

            Vector3 toTarget = (target.position - transform.position).normalized;

            if (other.CompareTag("Player") && rb.velocity.x == 0 && rb.velocity.y == 0)
            {
                if (Vector3.Dot(toTarget, transform.right) > 0)
                {
                    rb.AddForce(transform.up * thrust);
                    rb.AddForce(transform.right * -thrust);
                    transform.Rotate(0f, 180f, 0f);
                }
                else
                {
                    rb.AddForce(transform.up * thrust);
                    rb.AddForce(transform.right * thrust);
                }

                anim.SetBool("isJumping", true);
                StartCoroutine(SmallWait());
            }
        }
    }


    IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isJumping", false);
    }

}
