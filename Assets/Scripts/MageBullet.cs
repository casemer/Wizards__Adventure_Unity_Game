using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MageBullet : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 50;
    public float lifetime = 5f;
    public GameObject impactEffect;

    public float thrust = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.DamagePlayer(damage);

            }

            GameObject impact = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(impact, .3333f);
        }

    }



    private void Update()
    {
        Destroy(gameObject, .25f);
    }

}

