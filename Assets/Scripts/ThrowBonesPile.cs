using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBonesPile : MonoBehaviour {

    public GameObject player;
    public Transform firePoint;
    public GameObject bone;
    public float speed = 1f;
    private bool canAttack;
    public SkeletonBossAI boss;

    public GameObject dialogue;
    public Collider2D pd;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!boss.isDead && other.CompareTag("Player") && !dialogue.activeSelf && !pd.enabled)
        {

            if (canAttack)
            {
                StartCoroutine(WaitTime());
                FindObjectOfType<AudioManager>().Play("BoneThrow");
                Vector2 target = (new Vector2(player.transform.position.x, player.transform.position.y));
                Vector2 myPos = new Vector2(firePoint.position.x, firePoint.position.y);
                Vector2 direction = target - myPos;
                direction.Normalize();
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                GameObject projectile = (GameObject)Instantiate(bone, myPos, rotation);
                projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }
    }

    IEnumerator WaitTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }

}
