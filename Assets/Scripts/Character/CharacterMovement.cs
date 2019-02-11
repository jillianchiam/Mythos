using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    // Player Movement fields
    [SerializeField] private float speed = 15f;
    private float moveHorizontal;

    // Jump mechanic tweeking variables
    [SerializeField] private float jumpVelocity = 1500f;
    [SerializeField] private float fallMultiplier = 600f;
    [SerializeField] private float lowJumpMultiplier = 300f;
    [SerializeField] private float xWallDampingFactor = 100f;
    [SerializeField] private float wallJumpTime = 0.15f;

    private float gravity;

    // Conditional Statements for animations
    public bool onGround;
    private bool isJumping;
    public bool onVerticalSurface;

    // Conditional statements for mechanics
    private bool wallJumpAllowed;
    private bool doubleJumpEnable;
    private bool verticalJumpAllowed;
    private bool horizontalControl = true;

    // GameObject related fields
    private Animator animations;
    private Rigidbody2D rb2d;

    // Class initialization function
    void Start()                                                                        
    {
        rb2d = GetComponent<Rigidbody2D>();                                             // Attach the rigid body to the player
        animations = GetComponent<Animator>();                                          // Attach the animations to the player
        gravity = Physics2D.gravity.y;                                                  // Set gravity to physics engine gravity constant
    }

    void Update()                                                                       // Update is called once per frame
    {
        SetSpriteDirection();                                                           // Set sprite direction based on input
        RunAnimations();                                                                // Update animation booleans
        JumpMechanics();                                                                // Update jump parameters
    }

    // Update called on fixed time interval
    void FixedUpdate()                                                                  
    {
        ApplyPlayerMovement();                                                          // Accept inputs for horizontal movement

        if (verticalJumpAllowed)                                                        // If jump input and either on ground or double jump enabled
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);                            // Zero the y velocity before adding upward force
            rb2d.AddForce(Vector2.up * jumpVelocity);                                   // Add upwards force for jump on character
        }
        else if (wallJumpAllowed)                                                       // Off ground and currently on a wall
        {
            wallJumpAllowed = false;                                                    // Moving off wall so disable wall jump
            horizontalControl = false;                                                  // Disable user horizontal control

            Invoke("ReEnableHorizontalControl", wallJumpTime);                                  // Re enable horizontal control after set time

            if (transform.localScale.x == -1f)                                          // If wall is on left side of character
            {
                transform.localScale = new Vector3(1, 1, 1);                            // Flip sprite to match movement
                rb2d.velocity = new Vector2(transform.localScale.x * speed, 0);         // Apply velocity to send right
            }
            else                                                                        // If wall is on right side of character
            {
                transform.localScale = new Vector3(-1, 1, 1);                            // Flip sprite to match movement
                rb2d.velocity = new Vector2(transform.localScale.x * speed, 0);         // Apply velocity to send left
            }

            rb2d.AddForce(Vector2.up * jumpVelocity);                                   // Apply vertical force for jump
        }
        
        verticalJumpAllowed = false;                                                    // Jump applied so set back to false

        if (!horizontalControl && (rb2d.velocity.x * transform.localScale.x) > speed)   // If wall jump in effect and character is moving faster than walking speed
        {
            if (transform.localScale.x == -1f)
                rb2d.AddForce(Vector2.right * (xWallDampingFactor) * Time.deltaTime);   // Apply a greater downwards force to decrease rise faster
            else
                rb2d.AddForce(Vector2.left * (xWallDampingFactor) * Time.deltaTime);    // Apply a greater downwards force to decrease rise faster
        }

        if (rb2d.velocity.y < 0 && !onGround)                                           // Check if falling and airborne
            rb2d.AddForce(Vector2.up * gravity * (fallMultiplier) * Time.deltaTime);    // Apply greater froce to reduce floaty feeling
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump") && !onGround)          // Check if jump has been let go
            rb2d.AddForce(Vector2.up * gravity * (lowJumpMultiplier) * Time.deltaTime); // Apply a greater downwards force to decrease rise faster
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
            
            if (onGround)                                                               // If player is grounded apply character jump
            {
                onGround = false;
                doubleJumpEnable = true;
                verticalJumpAllowed = true;
            }
            else if (onVerticalSurface)                                                 // If player is on a vertical surface a wall jump is triggered
            {
                onVerticalSurface = false;
                doubleJumpEnable = true;
                wallJumpAllowed = true;
            }
            else if (doubleJumpEnable)                                                  // If doublejump is enabled the player may jump again
            {   
                doubleJumpEnable = false;
                verticalJumpAllowed = true;
            }
        }
    }

    // Change player velocity based on user input
    void ApplyPlayerMovement()                                                          
    {
        moveHorizontal = Input.GetAxis("Horizontal");                                   // Get horizonatl movement direction input

        if (horizontalControl && moveHorizontal != 0f)
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);       // Maintain y velocity and set x velocity to user input

    }

    // Re enables horizontal input capabilities
    void ReEnableHorizontalControl()
    {
        horizontalControl = true;
    }

    // Sets direction of player sprite
    void SetSpriteDirection()                                                           
    {
        if (moveHorizontal < -.1 && horizontalControl)
            transform.localScale = new Vector3(-1, 1, 1);                               // Make sprite face left
        else if (moveHorizontal > .1 && horizontalControl)
            transform.localScale = new Vector3(1, 1, 1);                                // Make sprite face right
    }

    // Ensures that Animation booleans are associated on each update
    void RunAnimations()                                                                
    {
        animations.SetFloat("Movement", Mathf.Abs(moveHorizontal));
        animations.SetBool("IsJumping", isJumping);
        animations.SetBool("OnGround", onGround);
    }
}
