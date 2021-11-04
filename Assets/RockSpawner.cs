using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

    public GameObject rock;
    public float spawnTime = 1f;
    public bool fromTop = false;

    void Start()
    {
        InvokeRepeating("addRock", spawnTime, spawnTime);
    }

    // this will handle instantiating objects from the top or right
    void addRock()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);

        if (fromTop)
        {
            float x1 = 38f;
            float x2 = 56f;

            // Randomly pick a x point within the spawn object
            spawnPoint.x = Random.Range(x1, x2);
        }
        else
        {
            float y1 = transform.position.y - GetComponent<Renderer>().bounds.size.y / 2;
            float y2 = transform.position.y + GetComponent<Renderer>().bounds.size.y / 2;

            // Randomly pick a y point within the spawn object
            spawnPoint.y = Random.Range(y1, y2);
        }
        Instantiate(rock, spawnPoint, Quaternion.identity);
    }
}