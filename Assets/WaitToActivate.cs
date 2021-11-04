using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToActivate : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("moveDown", true);
    }
}
