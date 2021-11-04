using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantPunch : MonoBehaviour
{
    public Animator anim;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    public Transform attackPoint1;
    public float attackRange = 0.5f;

    public Transform giant;
    bool hasAttacked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasAttacked)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("isPunching", true);
        hasAttacked = true;
        yield return new WaitForSeconds(0.33f);

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().DamagePlayer(attackDamage);

        }

        yield return new WaitForSeconds(0.33f);

        hasAttacked = false;
        anim.SetBool("isPunching", false);
    }
}
