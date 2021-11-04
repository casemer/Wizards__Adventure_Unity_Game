using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{

    public bool isDead = false;
    public GameObject blackFade;
    public StartDialogue sd;
    public GameObject talkBubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            isDead = true;
            sd.enabled = false;
            talkBubble.SetActive(false);
            StartCoroutine(FadeDelay());
        }
    }

    IEnumerator FadeDelay()
    {
        yield return new WaitForSeconds(1f);
        blackFade.SetActive(true);
    }
}
