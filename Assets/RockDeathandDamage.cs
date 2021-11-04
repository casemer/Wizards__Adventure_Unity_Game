using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDeathandDamage : MonoBehaviour
{

    public int attackDamage = 15;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteRock());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().DamagePlayer(attackDamage);
        }
    }

    IEnumerator DeleteRock()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
