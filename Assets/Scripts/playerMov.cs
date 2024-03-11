using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMov : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5;

    private float movingInput;

    [SerializeField] private bool FacingRight = true;
    [SerializeField] private float jumpForce = 12;

    private bool canMove = true;

    private bool canWallJump = true;
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool isWallSliding;
    AudioSource jumpSound;

    private int facingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask WhatIsGround;
    private bool isGrounded;

    [SerializeField] private Transform wallcheck;
    [SerializeField] private float wallcheckDistance;
    private bool isWallDetected;

    private Collider2D enemyCollider;
    public string ignoreColliderTag = "enemyBoundary";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckInput();
        FlipController();
        CollisionCheck();

    }

    private void FixedUpdate()
    {
        if (isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.01f);
        }
        else
        {
            isWallSliding = false;
            Move();
        }
        if (isGrounded)
        {
            canMove = true;
            canDoubleJump = true;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
        if (canMove)
            movingInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        if (canMove)
            rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
    }

    private void WallJump()
    {
        canMove = false;
        Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    private void JumpButton()
    {
        if (isWallSliding && canWallJump)
        {
            WallJump();
        }
        else if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpSound.Play();
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (isGrounded && isWallDetected)
        {
            if (FacingRight && movingInput < 0)
                Flip();
            else if (!FacingRight && movingInput > 0)
                Flip();
        }
        if (rb.velocity.x > 0 && !FacingRight)
            Flip();
        else if (rb.velocity.x < 0 && FacingRight)
            Flip();
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        // Adjust ray direction based on facing direction
        Vector2 rayDirection = FacingRight ? Vector2.right : Vector2.left;

        // Cast a ray to check for walls
        isWallDetected = Physics2D.Raycast(wallcheck.position, rayDirection, wallcheckDistance, WhatIsGround);

        if (!isGrounded && rb.velocity.y < 0)
            canWallSlide = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("death"))
        {
            SceneManager.LoadScene("deathScene");
        }
        if (collision.CompareTag(ignoreColliderTag))
        {
            Debug.Log("Trigger Enter: " + collision.gameObject.name);
            DisableCollisionsWithPlayer(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ignoreColliderTag))
        {
            EnableCollisionsWithPlayer(collision.gameObject);
        }
    }

    private void DisableCollisionsWithPlayer(GameObject playerObject)
    {
        Collider2D[] playerColliders = playerObject.GetComponents<Collider2D>();
        foreach (Collider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);
        }
    }

    private void EnableCollisionsWithPlayer(GameObject playerObject)
    {
        Collider2D[] playerColliders = playerObject.GetComponents<Collider2D>();
        foreach (Collider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(enemyCollider, playerCollider, false);
        }
    }
}