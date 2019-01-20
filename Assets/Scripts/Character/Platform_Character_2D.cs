using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Platform_Character_2D : MonoBehaviour {

    // Player Movement fields
    public float speed;
    public float jumpPower;
    private float moveHorizontal;
    private Rigidbody2D rb2d;

    // Animation related fields
    private Animator animations;
    private bool doubleJumpEnable;
    private bool onGround;
    private bool isJumping;

	// Use this for initialization
	void Start () {
        // Attach the rigid body and animations to the player
        rb2d = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        RunAnimations();
        ApplyPlayerMovement();
	}

    // Change player velocity based on user input
    void ApplyPlayerMovement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        SetSpriteDirection();
        Jump();
    }

    // Sets direction of player sprite
    void SetSpriteDirection()
    {
        // Face left
        if (moveHorizontal < -.1)
            transform.localScale = new Vector3(-1, 1, 1);
        // Face right
        else if (moveHorizontal > .1)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            if (onGround || doubleJumpEnable)
            {
                isJumping = true;
            }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            if (onGround)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                onGround = false;
                doubleJumpEnable = true;
            }
            else if (doubleJumpEnable)
            {
                doubleJumpEnable = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpPower);
            }
        }
    }
    
    // Checks whether player is touching ground or not
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }
    
    // Ensures that Animation booleans are associated on each update
    void RunAnimations()
    {
        animations.SetFloat("Movement", Mathf.Abs(moveHorizontal));
        animations.SetBool("IsJumping", isJumping);
        animations.SetBool("OnGround", onGround);
    }
}
