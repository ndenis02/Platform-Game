using System;
using System.Collections;
using System.Collections.Generic;
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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        checkInput();
        flipController();
        CollisionCheck();

    }



    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpSound.Play();
    }

    private void FixedUpdate()
    {
        if (isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -3.5f));
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

    private void checkInput()
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

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }
    private void flipController()
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Vector3 lineDirection = FacingRight ? Vector3.right : Vector3.left;

        // Draw the line in the determined direction
        Gizmos.DrawLine(wallcheck.position, wallcheck.position + lineDirection * wallcheckDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "death")
        {
            SceneManager.LoadScene("deathScene");
        }

        //else if (collision.tag == "death2")
        //{
        //    SceneManager.LoadScene("deathScene2");
        //}
    }

}
