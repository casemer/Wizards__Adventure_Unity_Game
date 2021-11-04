using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{

    Rigidbody2D body;

    float horizontal;
    float vertical;
    Animator anim;
    public float runSpeed = 3.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical > 0.1f) {
            anim.SetFloat("ClimbSpeed", Mathf.Abs(vertical * 2));
                }
        else if (horizontal > 0.1f)
        {
            anim.SetFloat("ClimbSpeed", Mathf.Abs(horizontal * 2));
        }
        else if (vertical < 0.0f)
        {
            anim.SetFloat("ClimbSpeed", Mathf.Abs(vertical * 2));
        }
        else if (horizontal < 0.0f)
        {
            anim.SetFloat("ClimbSpeed", Mathf.Abs(horizontal * 2));
        }
        else
        {
            anim.SetFloat("ClimbSpeed", 0);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
