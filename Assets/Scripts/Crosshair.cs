using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public GameObject player;
    public GameObject crosshairs;
    private Vector3 target;
    public GameObject bullet;
    public Transform firePoint;
    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            target = transform.GetComponent<Camera>().ScreenToWorldPoint
                (new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

            crosshairs.transform.position = new Vector2(target.x, target.y);

            if (Input.GetButtonDown("Fire1"))
            {
                FindObjectOfType<AudioManager>().Play("Fire");
                Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                Vector2 myPos = new Vector2(firePoint.position.x, firePoint.position.y);
                Vector2 direction = target - myPos;
                direction.Normalize();
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                GameObject projectile = (GameObject)Instantiate(bullet, myPos, rotation);
                projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }

        }
    }
}
