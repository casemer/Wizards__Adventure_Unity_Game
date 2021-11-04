using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    public GameObject sceneTransition;
    public Skull skull;
    public Animator anim;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        sceneTransition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (skull.isDead && counter < 1)
        {
            anim.SetBool("isOpen", true);
            FindObjectOfType<AudioManager>().Play("Door");
            counter++;
            sceneTransition.SetActive(true);
        }

    }

}
