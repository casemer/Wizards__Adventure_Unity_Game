using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float knockbackForce = 8f;

    public float moveSpeed;
    public float jumpForce;

    public bool jumpSound;

    private Rigidbody2D rb;

    public Transform groundCheckPoint, groundCheckPoint2;
    public LayerMask whatIsGround;
    private bool isGrounded;

    public Animator anim;
    public SpriteRenderer playerSR;

    public float hangTime = .1f;
    private float hangCounter;

    public float jumpBufferLength = .01f;
    private float jumpBufferCount;

    public ParticleSystem footSteps;
    private ParticleSystem.EmissionModule footEmission;

    public ParticleSystem impactEffet;
    private bool wasOnGround;
    public VectorValue startingPos;
    public new UnityEngine.Experimental.Rendering.Universal.Light2D light;

    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPos.initialValue;
        rb = GetComponent<Rigidbody2D>();
        footEmission = footSteps.emission;
        StartCoroutine(JumpSoundPause());
        facingRight = transform.localScale.x > 0;
        footEmission.rateOverTime = 0f;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "ForestPond")
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }

    }

    IEnumerator JumpSoundPause()
    {
        jumpSound = false;
        yield return new WaitForSeconds(0.1f);
        jumpSound = true;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .1f, whatIsGround) ||
            Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);

        float velocity = Input.GetAxis("Horizontal") * moveSpeed;
        anim.SetFloat("Speed", Mathf.Abs(velocity));

        //light
        if (Input.GetKeyUp(KeyCode.F))
        {
            light.enabled = !light.enabled;
        }


        //hangtime manager
        if (isGrounded)
        {
            hangCounter = hangTime;
            anim.SetBool("IsOnGround", true);
            anim.SetBool("IsFalling", false);
        }
        else
        {
            hangCounter -= Time.deltaTime;
            anim.SetBool("IsOnGround", false);
        }

        //jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        //jump
        if (jumpBufferCount >= 0 && hangCounter > 0 && rb.velocity.y < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump");
            jumpBufferCount = 0;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //jump animation sets
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






        //flip

        if (groundCheckPoint.transform.position.x > groundCheckPoint2.transform.position.x)
        {
            facingRight = true;
        }

        if (groundCheckPoint.transform.position.x < groundCheckPoint2.transform.position.x)
        {
            facingRight = false;
        }

        if (rb.velocity.x > 0 && !facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            //facingRight = true;
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            //facingRight = false;
        }


        //particle stuff
        if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
        {
            footEmission.rateOverTime = 20f;
        }
        else
        {
            footEmission.rateOverTime = 0f;
        }

        //impact particle effect
        if (!wasOnGround && isGrounded && jumpSound && rb.velocity.y <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Landing");
            impactEffet.gameObject.SetActive(true);
            impactEffet.Stop();
            impactEffet.transform.position = footSteps.transform.position;
            impactEffet.Play();
        }

        wasOnGround = isGrounded;
    }

}
