using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public int healAmount = 20;
    public GameObject healthExplosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().HealPlayer(healAmount);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(gameObject);
            GameObject impact = (GameObject)Instantiate(healthExplosion, transform.position, transform.rotation);
            Destroy(impact, .25f);
        }
    }


}
