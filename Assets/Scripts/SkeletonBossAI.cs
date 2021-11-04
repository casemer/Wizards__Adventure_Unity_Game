using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossAI : MonoBehaviour {
    public int maxHealth = 1000;
    public int currentHealth;
    public Transform enemyGFX;

    public Animator anim;
    public Animator bossHealthBar;

    public Rigidbody2D rb;

    public GameObject ghostDeath;

    public bool isDead;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
    }




    void Die()
    {
        FindObjectOfType<AudioManager>().Play("GhostDeath");
        Destroy(gameObject);
        GameObject impact = (GameObject)Instantiate(ghostDeath, transform.position, transform.rotation);
        Destroy(impact, .25f);
    }


    public void TakeDamage(int damage)
    {


        currentHealth -= damage;
        StartCoroutine(HurtAnim());
        healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
            bossHealthBar.SetBool("dropDown", false);
        }

    }

    IEnumerator HurtAnim()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(.10f);
        anim.SetBool("Hit", false);
    }

}
