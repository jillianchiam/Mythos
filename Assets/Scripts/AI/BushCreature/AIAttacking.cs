﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacking : MonoBehaviour {
    
    [SerializeField] private Vector2 liftVelocity;                                              // liftoff velocity
    [SerializeField] private float fallMultiplier = 600f;                                       // Downwards physics multiplier
    [SerializeField] private float lowFallMultiplier = 300f;                                    // Downwards physics smoother

    private GameObject attackRangeObject;                                                       // To hold child gameobject reference
    private CircleCollider2D attackRange;                                                       // CircleCollider -> Is a child of the enemy gameobject

    public bool playerInRange;                                                                  // True if player is in AttackRange collider
    private bool attacking;                                                                     // True if enemy should try to attack
    private bool directionSet;                                                                  // Has direction been decided yet
    private bool stopPhysics;                                                                   // End velocity settings if colliding

    private Rigidbody2D rb2d;                                                                   // Enemy's rigidbody
    private Animator animations;                                                                // Enemy's animations

    private Transform player;                                                                   // A reference to the player for raycasting
    private Vector3 lineOfSightEnd;                                                             // Enemies line of sight
    private float scaleFactor;                                                                  // Scale of the enemy set in unity (Assumes x, y, z all scaled by same factor)
    private AnimatorStateInfo aniStateInfo;                                                     // Store current animation state information
    private float gravity;

    // Use this for initialization
    void Start()
    {
        attackRangeObject = this.gameObject.transform.GetChild(1).gameObject;                   // Second child is the AttackRange object
        attackRange = attackRangeObject.GetComponent<CircleCollider2D>();                       // Attach child's collider  to a variable
        rb2d = GetComponent<Rigidbody2D>();                                                     // Get the enemies rigidbody
        animations = GetComponent<Animator>();                                                  // Attach the animations to the player
        player = GameObject.Find("Player").transform;                                           // Get a reference to the player transform
        playerInRange = false;                                                                  // Initialize to false since player not in range
        stopPhysics = false;
        scaleFactor = transform.localScale.x;
        gravity = Physics2D.gravity.y;
    }

    // Update is called once per frame
    void Update () {
        RunAnimations();                                                                        // Update animation parameters
    }

    void FixedUpdate ()
    {
        aniStateInfo = animations.GetCurrentAnimatorStateInfo(0);                               // Used multiple times so store this

        if (playerInRange && !attacking)
            attacking = true;                                                                   // Start the attack

        if (attacking)
        {             
            if (aniStateInfo.IsName("liftoff"))
            {
                if (!directionSet)                                                              // Only want to run this function once
                    GetProperDirection();                                                       // Configure looking and jumping direction
                ApplyLiftoff();                                                                 // Generate liftoff velocity
            }
            else if (aniStateInfo.IsName("airborne"))
            {
                ApplyPhysics();
            }
            else if (aniStateInfo.IsName("landing"))
            {
                ParameterReset();                                                               // Reset boolean logic and rigidbody settings
                this.gameObject.GetComponent<AIMovement>().awake = true;
            }
        }
    }

    void ParameterReset()
    {
        rb2d.gravityScale = 50;                                                                 // Reset gravity scale
        attacking = false;                                                                      // Done jumping
        directionSet = false;                                                                   // Allow direction to be computed next jump
        stopPhysics = false;                                                                    // Allow reapplication of physics for next jump
    }

    void GetProperDirection ()
    {
        if (player.transform.position.x < transform.position.x)                                 // Player on left side
        {
            transform.localScale = new Vector3(1, 1, 1) * scaleFactor;                          // Enemy hand on left side
            if (liftVelocity.x > 0)
                liftVelocity = new Vector2(-liftVelocity.x, liftVelocity.y);                    // Flip velocity's x component so it goes left
        }       
        else if (player.transform.position.x > transform.position.x)                            // Player on right side
        {
            transform.localScale = new Vector3(-1, 1, 1) * scaleFactor;                         // Enemy hand on right side
            if (liftVelocity.x < 0)
                liftVelocity = new Vector2(-liftVelocity.x, liftVelocity.y);                    // Flip velocity's x component so it goes left
        }

        directionSet = true;                                                                    // Only update direction 1 time per attack
    }

    void ApplyLiftoff()
    {
        rb2d.gravityScale = 10;
        rb2d.velocity = liftVelocity;
    }

    void ApplyPhysics()
    {
        if (!stopPhysics)
            rb2d.velocity = new Vector2(liftVelocity.x, rb2d.velocity.y);                       // Only apply if not colliding with anything

        if (rb2d.velocity.y < 0)
            rb2d.AddForce(Vector2.up * gravity * (fallMultiplier) * Time.deltaTime);            // Adding force when its going up
        else if (rb2d.velocity.y > 0)
            rb2d.AddForce(Vector2.up * gravity * (lowFallMultiplier) * Time.deltaTime);         // Falls with less floaty-ness
    }

    void RunAnimations ()
    {
        animations.SetBool("Attacking", attacking);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (aniStateInfo.IsName("airborne"))
            stopPhysics = true;
    }
}
