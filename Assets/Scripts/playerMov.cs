using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMov : MonoBehaviour
{
    //movement values
    float h;
    public float speed;
    Rigidbody2D rb;
    public float wallDetectionDistance = 0.2f;

    public float jumpforce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;
    bool jump;

    [Header("Wall Jump System")]
    public Transform wallCheck;
    bool isWallTouch;
    bool isSliding;
    public float wallSliddingSpeed;
    public float wallJumpDuration;
    public Vector2 WallJumpForce;
    bool WallJumping;

    //Rigidbody reference

    //jump check parameters

    //Sounds
    AudioSource jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Start()
    {
        jumpSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f), 0, groundLayer);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 1f), 0, groundLayer);
       

        if (isWallTouch && !isGrounded && h !=0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
        flip();
    }

    private void FixedUpdate()
    {

        if (jump)
        {
            Jump();
            jumpSound.Play();
        }
        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSliddingSpeed, float.MaxValue));
        }

        if (WallJumping)
        {
            rb.velocity = new Vector2(-h * WallJumpForce.x, WallJumpForce.y);
        }
        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }
    }


    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
        else if (isSliding)
        {
            WallJumping = true;
            Invoke("StopWallJumping", wallJumpDuration);
        }
        jump = false;
    }
    void StopWallJumping()
    {
        WallJumping = false;
    }

    void flip()
    {
        if (h < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (h > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "death")
        {
            SceneManager.LoadScene("deathScene");
        }
    }
}