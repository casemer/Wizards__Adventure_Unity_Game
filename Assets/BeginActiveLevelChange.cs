using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginActiveLevelChange : MonoBehaviour
{
    public GameObject levelChanger;

    // Start is called before the first frame update
    void Start()
    {
        levelChanger.SetActive(true);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        levelChanger.SetActive(false);
    }
}
