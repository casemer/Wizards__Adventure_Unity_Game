using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerMovement : MonoBehaviour {

    // Move player in 2D space
    public float maxSpeed = 2f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public float airDrag = 0.8f;
    public Camera mainCamera;
    public Animator anim;
    public float knockbackForce = 8f;
    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;

    bool jumpDelay = false;
    public VectorValue startingPos;

    public new UnityEngine.Experimental.Rendering.Universal.Light2D light;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D rb;
    Collider2D mainCollider;
    // Check every collider except Player and Ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    Transform t;


    // Use this for initialization
    void Start()
    {
        transform.position = startingPos.initialValue;
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        gameObject.layer = 8;

        if (mainCamera)
            cameraPos = mainCamera.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }

        else
        {
            if (isGrounded || rb.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            light.enabled = !light.enabled;
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                //t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
                t.Rotate(0f, 180f, 0f);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                //t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                t.Rotate(0f, 180f, 0f);
            }
        }

        float velocity = Input.GetAxis("Horizontal") * maxSpeed;

        anim.SetFloat("Speed", Mathf.Abs(velocity));

        if (!isGrounded)
        {
            velocity *= airDrag;
            anim.SetBool("IsOnGround", false);
            StartCoroutine(CoyoteTime());
        }

        if (isGrounded)
        {
            anim.SetBool("IsOnGround", true);
            anim.SetBool("IsFalling", false);
        }

        // Initiate Jump
        if (Input.GetButton("Jump") && !jumpDelay)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            FindObjectOfType<AudioManager>().Play("Jump");
            StartCoroutine(JumpDelay());

        }

        IEnumerator JumpDelay()
        {
            jumpDelay = true;
            yield return new WaitForSeconds(.7f);
            jumpDelay = false;
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }

        // Cancel Jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    void FixedUpdate()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        // Apply movement velocity
        rb.velocity = new Vector2((moveDirection) * maxSpeed, rb.velocity.y);



    }

    IEnumerator CoyoteTime()
    {
        jumpDelay = true;
        yield return new WaitForSeconds(.5f);
        jumpDelay = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Vector2 difference = transform.position - other.transform.position;
            Vector2 force = difference * knockbackForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}