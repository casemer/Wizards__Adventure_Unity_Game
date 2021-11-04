using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDeath : MonoBehaviour
{
    public GameObject acidDeath;
    private int acidDamage = 9999;
    public GameObject player;
    public bool diedByAcid = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            
            other.GetComponent<Player>().DamagePlayer(acidDamage);
            GameObject acid = (GameObject)Instantiate(acidDeath, 
                new Vector3(player.transform.position.x, other.transform.position.y + .5f, other.transform.position.z), 
                player.transform.rotation);
            FindObjectOfType<AudioManager>().Play("AcidDeath");
            diedByAcid = true;
        }
    }
}
