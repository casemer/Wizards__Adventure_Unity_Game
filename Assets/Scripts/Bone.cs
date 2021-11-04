using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bone : MonoBehaviour {

    public float speed = 10f;
    public Rigidbody2D rb;
    public int damage = 10;
    public float lifetime = 5f;
    public float knockbackForce = 50f;


    public Rigidbody2D bullet;
    //public GameObject impactEffect;

    public float thrust = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                    player.DamagePlayer(damage);
                    Debug.Log(damage);
            }

            SkeletonBossAI boss = other.GetComponent<SkeletonBossAI>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            //add skeleton damage here as well;

            //GameObject impact = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            //Destroy(impact, .3333f);
        }

        if (other.tag == "Bullet")
        {
            rb.velocity = other.GetComponent<Rigidbody2D>().velocity / 2;
        }  
    }

}

