using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMov : MonoBehaviour
{
    //movement values
    public int walkSpeed;
    public int sprintSpeed;
    public int jumpHeight;
    
    //Rigidbody reference
    Rigidbody2D rb;

    //jump check parameters
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGrounded;

    //Sounds
    AudioSource jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);

        //sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(move * sprintSpeed * Time.deltaTime);
        }
        //walk
        else
        {
            transform.Translate(move * walkSpeed * Time.deltaTime);
        }

        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundMask);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpSound.Play();
        }
    }
}
