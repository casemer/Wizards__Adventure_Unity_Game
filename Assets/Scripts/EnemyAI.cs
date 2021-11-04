using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Security.Cryptography;
using System.Threading;

public class EnemyAI : MonoBehaviour {
    public LayerMask playerLayer;
    public int maxHealth = 100;
    int currentHealth;
    public float knockbackForce = 50f;
    public int attackDamage = 1;
    public Transform attackPoint1;
    public float attackRange = 0.5f;
    public Transform enemyGFX;

    public Animator anim;

    public Rigidbody2D rb;

    public GameObject ghostDeath;

    public float senseRange = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    void UpdatePath()
    {

        if(seeker.IsDone() && target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }




    void Die()
    {
        FindObjectOfType<AudioManager>().Play("GhostDeath");
        Destroy(gameObject);
        GameObject impact = (GameObject)Instantiate(ghostDeath, transform.position, transform.rotation);
        Destroy(impact, .25f);
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().DamagePlayer(attackDamage);
        }

    }


    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= senseRange)
            {

                Invoke("Attack", 1f);

                if (path == null) { return; }

                if (currentWaypoint >= path.vectorPath.Count)
                {
                    return;
                }


                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                rb.AddForce(force);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWayPointDistance)
                {
                    currentWaypoint++;
                }
            }
        }

        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }

    }


    public void TakeDamage(int damage)
    {


        currentHealth -= damage;
        StartCoroutine(HurtAnim());



        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    IEnumerator HurtAnim()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(.20f);
        anim.SetBool("Hit", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
           Vector2 difference = transform.position - other.transform.position;
            Vector2 force = difference * knockbackForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }


    //THIS IS FOR EVERYTHING ELSE BRACKEYS YOU GOD

    public Transform target;
    public float speed = 20f;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }



}
