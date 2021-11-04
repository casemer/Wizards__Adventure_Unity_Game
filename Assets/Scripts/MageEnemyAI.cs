using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Security.Cryptography;
using System.Threading;

public class MageEnemyAI : MonoBehaviour {
    public LayerMask playerLayer;
    public int maxHealth = 100;
    int currentHealth;
    public float knockbackForce = 2f;
    public int attackDamage = 1;
    public Transform enemyGFX;

    public Animator anim;

    public Rigidbody2D rb;

    Transform t;

    public GameObject mageDeath;

    public float senseRange = 10f;

    bool isGrounded = false;
    bool isActuallyGrounded = false;
    bool isWall = false;
    public Transform groundCheck;
    public Transform realGroundCheck;
    public Transform wallCheck, wallCheck2;
    public LayerMask groundMask;
    public LayerMask wallMask;
    public float groundDistance = 0.2f;
    public float groundDistance2 = 0.01f;
    Collider2D mainCollider;
    bool hasJumped = false;
    bool facingRight = true;
    float velocity;


    void Start()
    {
        t = transform;
        mainCollider = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    void UpdatePath()
    {

        if (seeker.IsDone() && target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }




    void Die()
    {
        FindObjectOfType<AudioManager>().Play("GhostDeath");
        Destroy(gameObject);
        GameObject impact = (GameObject)Instantiate(mageDeath, transform.position, transform.rotation);
        Destroy(impact, .25f);
    }


    void FixedUpdate()
    {
        isActuallyGrounded = Physics2D.OverlapCircle(realGroundCheck.position, groundDistance2, groundMask);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
        isWall = (Physics2D.OverlapCircle(wallCheck.position, groundDistance, groundMask)) ||
            (Physics2D.OverlapCircle(wallCheck.position, groundDistance, wallMask)) ||
            (Physics2D.OverlapCircle(wallCheck2.position, groundDistance, groundMask)) ||
            (Physics2D.OverlapCircle(wallCheck2.position, groundDistance, wallMask));

        velocity = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("Speed", velocity);


        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= senseRange)
            {

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


            if (target.transform.position.y >= rb.transform.position.y - 0.2f && !isGrounded && isActuallyGrounded)
            {
                if (!hasJumped)
                {
                    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                    Vector2 force = direction * speed * Time.deltaTime;
                    rb.AddForce(Vector2.up * 400);
                }
                hasJumped = true;

            }

            if (target.transform.position.y > rb.transform.position.y && isWall && isActuallyGrounded)
            {

                if (!hasJumped)
                {
                    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                    Vector2 force = direction * speed * Time.deltaTime;

                    rb.AddForce(Vector2.up * 2000);
                }
                hasJumped = true;

            }

            if (!isActuallyGrounded)
            {
                anim.SetBool("isJumping", true);
            }

            if (isGrounded)
            {
                hasJumped = false;
                anim.SetBool("isJumping", false);
            }


            if (rb.velocity.x >= 0.1f && !facingRight)
            {
                t.Rotate(0f, 180f, 0f);
                facingRight = !facingRight;
            }
            if (rb.velocity.x <= 0.1f && facingRight)
            {
                t.Rotate(0f, 180f, 0f);
                facingRight = !facingRight;

            }
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
        if (other.tag == "Bullet")
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
