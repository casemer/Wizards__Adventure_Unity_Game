using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image tutorial;
    public Animator anim;
    private bool hasSeenIt;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.enabled = false;
        hasSeenIt = false;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSeenIt)
        {
            StartCoroutine(EnableTut());
            hasSeenIt = true;
        }
    }

    IEnumerator EnableTut()
    {
        
        tutorial.enabled = true;
        anim.Play("Tutorialfadein");
        yield return new WaitForSeconds(4f);
        anim.Play("Tutorialfadeout");
        yield return new WaitForSeconds(1f);
        tutorial.enabled = false;
    }



}
