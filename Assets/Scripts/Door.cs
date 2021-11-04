using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Collider2D doorActivate;
    public Collider2D doorCol;
    private bool playerInRange;
    private bool openState = false;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            openState = !openState;
            anim.SetBool("isOpen", openState);
            FindObjectOfType<AudioManager>().Play("Door");
        }

        if (anim.GetBool("isOpen"))
        {
            doorCol.enabled = false;
        }
        else
        {
            doorCol.enabled = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
