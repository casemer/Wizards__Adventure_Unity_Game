using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteRock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeleteRock()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
