using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformDrop : MonoBehaviour
{
    private PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButton("Down"))
        {
                effector.rotationalOffset = 180f;
            StartCoroutine(SmallDelay());
        }
    }

    IEnumerator SmallDelay()
    {
        yield return new WaitForSeconds(0.25f);
        effector.rotationalOffset = 0f;
    }

}
