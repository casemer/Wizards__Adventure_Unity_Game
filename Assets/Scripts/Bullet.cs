using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour {

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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Bone"))
        {
            EnemyAI ghost = other.GetComponent<EnemyAI>();
            if (ghost != null)
            {
                ghost.TakeDamage(damage);

            }
            ZombieEnemyAI zombie = other.GetComponent<ZombieEnemyAI>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);

            }

            MageEnemyAI mage = other.GetComponent<MageEnemyAI>();
            if (mage != null)
            {
                mage.TakeDamage(damage);

            }

            WitchEnemyAI witch = other.GetComponent<WitchEnemyAI>();
            if (witch != null)
            {
                witch.TakeDamage(damage);

            }

            GiantEnemyAI giant = other.GetComponent<GiantEnemyAI>();
            if (giant != null)
            {
                giant.TakeDamage(damage);

            }

            EnergyEnemyAI energy = other.GetComponent<EnergyEnemyAI>();
            if (energy != null)
            {
                energy.TakeDamage(damage);

            }

            GodAI god = other.GetComponent<GodAI>();
            if (god != null)
            {
                god.TakeDamage(damage);

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

