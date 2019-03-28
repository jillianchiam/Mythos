using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacking1 : MonoBehaviour
{

    [SerializeField] private Vector2 liftVelocity;                                              // liftoff velocity
    [SerializeField] private float fallMultiplier = 400f;                                       // Downwards physics multiplier
    [SerializeField] private float lowFallMultiplier = 80f;                                    // Downwards physics smoother

    private GameObject attackRangeObject;                                                       // To hold child gameobject reference
    private CircleCollider2D attackRange;                                                       // CircleCollider -> Is a child of the enemy gameobject

    private int health;

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

    private SpriteRenderer sprite;
    private Color originalColor;
    private Color damageColor;
    private bool iFrameCurrent;
    private int count;

    // Use this for initialization
    void Start()
    {
        attackRangeObject = this.gameObject.transform.GetChild(0).gameObject;                   // Second child is the AttackRange object
        attackRange = attackRangeObject.GetComponent<CircleCollider2D>();                       // Attach child's collider  to a variable
        rb2d = GetComponent<Rigidbody2D>();                                                     // Get the enemies rigidbody
        animations = GetComponent<Animator>();                                                  // Attach the animations to the player
        player = GameObject.Find("Player").transform;                                           // Get a reference to the player transform
        playerInRange = false;                                                                  // Initialize to false since player not in range
        stopPhysics = false;
        scaleFactor = transform.localScale.x;
        gravity = Physics2D.gravity.y;

        sprite = GetComponent<SpriteRenderer>();                                    // Attach sprite renderer to variable
        originalColor = sprite.color;                                               // Regular sprite color
        damageColor = Color.red;                                                    // Other sprite color
        count = 0;
        iFrameCurrent = false;                                                      // Can take damage

        if (scaleFactor > 0.7)
            health = 5;
        else
            health = 3;
    }

    void InvincibilityTimer()
    {
        iFrameCurrent = false;                                                      // iFrames over damage can be taken
        sprite.color = originalColor;                                               // Color needs to be original
        count = 0;                                                                  // Reset for next time damage is taken
    }

    // Update is called once per frame
    void Update()
    {
        RunAnimations();                                                                        // Update animation parameters
    }

    void FixedUpdate()
    {
        if (iFrameCurrent)                                                          // iFrames are present when damage was taken
        {
            if (count == 5)                                                         // Use a counter
            {
                if (sprite.color == originalColor)
                    sprite.color = damageColor;                                     // Switch color to show damage was taken
                else
                    sprite.color = originalColor;                                   // Switch back to original color

                count = 0;                                                          // Reset to delay next color swap
            }
            else
                count++;                                                            // Increment for next color swap
        }

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
                this.gameObject.GetComponent<AIMovement1>().awake = true;
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

    void GetProperDirection()
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
        rb2d.gravityScale = 5;
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

    void RunAnimations()
    {
        animations.SetBool("Attacking", attacking);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PickUp" && !iFrameCurrent)
        {
            iFrameCurrent = true;                                                   // Give player invincibility frames
            sprite.color = damageColor;                                             // Sprite set to show damage taken
            Invoke("InvincibilityTimer", 1.0f);                                     // Give player 1 second of immunity after taking a hit

            health--;

            if (health == 0)
            {
                GameObject.Find("Player").GetComponent<CharacterHealth>().enemiesSlain++;
                this.gameObject.SetActive(false);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (aniStateInfo.IsName("airborne"))
            stopPhysics = true;
    }
}
