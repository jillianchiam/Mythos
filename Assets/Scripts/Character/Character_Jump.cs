using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Jump : MonoBehaviour {

    // Jump mechanic tweeking variables
    [SerializeField] private float jumpVelocity = 1000f;
    [SerializeField] private float wallJumpPushVelocity = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    // Conditional Statements for animations
    private bool onGround;
    private bool isJumping;
    private bool onVerticalSurface;

    // Conditional statements for mechanics
    private bool wallJumpAllowed;
    private bool doubleJumpEnable;
    private bool verticalJumpAllowed;

    // Character objects
    private Animator animations;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        // Attach the rigid body and animations to the player
        rb2d = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        ApplyAnimations();
        JumpMechanics();
    }

    // Updates booleans to allow proper jump type
    void JumpMechanics()
    {
        // Update condition for proper animation transition
        if (Input.GetButtonDown("Jump") && (onGround || doubleJumpEnable))
            isJumping = true;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = false;

            // If player is grounded apply character jump
            if (onGround)
            {
                onGround = false;
                doubleJumpEnable = true;
                verticalJumpAllowed = true;
            }
            // If player is on a vertical surface a wall jump is triggered
            else if (onVerticalSurface)
            {
                onVerticalSurface = false;
                doubleJumpEnable = true;
                wallJumpAllowed = true;
            }
            // If doublejump is enabled the player may jump again
            else if (doubleJumpEnable)
            {
                doubleJumpEnable = false;
                verticalJumpAllowed = true;
            }
        }
    }

    // Update called on fixed time interval
    void FixedUpdate()
    {
        if (verticalJumpAllowed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpVelocity);
        }
        else if (wallJumpAllowed)
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(Vector2.up * jumpVelocity);
            rb2d.AddForce(Vector2.left * wallJumpPushVelocity);
        }
        verticalJumpAllowed = false;
        wallJumpAllowed = false;

        // Conditions below apply faster falling to reduce floaty feeling

        // Check if falling and in air
        if (rb2d.velocity.y < 0 && !onGround)
            rb2d.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime); // fallMultiplier - 1 accounts for physics systems normal gravity
        // Check if jump has been let go while in air
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump") && !onGround)
            rb2d.AddForce(Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
    }

    // Sets all physical collision flags
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            onVerticalSurface = false;
        }
        else if (collision.gameObject.CompareTag("Solid_Wall"))
        {
            onVerticalSurface = true;
            onGround = false;
        }
    }

    void ApplyAnimations()
    {
        animations.SetBool("IsJumping", isJumping);
        animations.SetBool("OnGround", onGround);
    }
}
