using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    [SerializeField] private CircleCollider2D fieldOfView;                                      // CircleCollider -> Is a child of the enemy gameobject
    [SerializeField] private float speed;                                                       // Movement speed of the enemy
    [SerializeField][Range(0f, 90f)] private float angleOfSight;                                // Angle of enemy sight from horizontal

    public bool playerInRange;                                                                  // True if player is in FOV collider
    private bool walking;                                                                       // True if enemy should have walking animation applied
    public bool awake;                                                                          // Should hide and attack and then consider moving

    private Rigidbody2D rb2d;                                                                   // Enemy's rigidbody
    private Animator animations;                                                                // Enemy's animations

    private Transform player;                                                                   // A reference to the player for raycasting
    private Vector3 lineOfSightEnd;                                                             // Enemies line of sight
    private float scaleFactor;                                                                  // Scale of the enemy set in unity (Assumes x, y, z all scaled by same factor)


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();                                                     // Get the enemies rigidbody
        animations = GetComponent<Animator>();                                                  // Attach the animations to the player
        player = GameObject.Find("Player").transform;                                           // Get a reference to the player transform
        playerInRange = false;                                                                  // Initialize to false since player not in range
        scaleFactor = transform.localScale.x;
        awake = false;
    }
	
	void Update () {
        RunAnimations();                                                                        // Update animation booleans
    }

    void FixedUpdate ()
    {
        // Code to process how to follow player
        if (playerInRange && awake)
        {
            if (PlayerInFieldOfView())                                                          // If player is within visible range
            {
                walking = true;                                                                 // Player in sight so set walking boolean true

                if (player.transform.position.x < transform.position.x && 
                    animations.GetCurrentAnimatorStateInfo(0).IsName("walkcycle"))              // Player on left side
                {
                    transform.localScale = new Vector3(1, 1, 1) * scaleFactor;                  // Enemy hand on left side
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);                       // Move left towards player
                }
                else if (player.transform.position.x > transform.position.x && 
                    animations.GetCurrentAnimatorStateInfo(0).IsName("walkcycle"))              // Player on right side
                {
                    transform.localScale = new Vector3(-1, 1, 1) * scaleFactor;                 // Enemy hand on right side
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);                        // Move right towards player
                }
                else
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);                            // Go stationary
            }
            else
                ApplyMovementCycle();                                                           // Player not in sight apply programmed walk cycle
        }
        else
            ApplyMovementCycle();                                                               // Player not in sight apply programmed walk cycle
    }

    void ApplyMovementCycle()
    {
        rb2d.velocity = Vector2.zero;
        walking = false;
    }

    bool PlayerInFieldOfView()
    {
        Vector2 directionToPlayer = player.position - transform.position;                       // Represents the direction from the enemy to the player

        if (transform.localScale.x == 1.0f)
            lineOfSightEnd = new Vector3(transform.position.x - 1, transform.position.y, 0);    // Get line of sight vector to be left side of enemy
        else
            lineOfSightEnd = new Vector3(transform.position.x + 1, transform.position.y, 0);    // Get line of sight vector to be right side of enemy

        Vector2 lineOfSight = lineOfSightEnd - transform.position;                              // The centre of the enemy's field of view, the direction of looking directly ahead
        float angle = Vector2.Angle(directionToPlayer, lineOfSight);                            // Calculate the angle formed between the player's position and the centre of the enemy's line of sight

        if ((angle < angleOfSight && angle > 0) || (angle < 180 && angle > 180 - angleOfSight)) // If player within line of sight angle check if boundaries are blocking vision
            return !PlayerHiddenByObstacles();                                                  // Determine if something is between the player and enemy that blocks sight
        else
            return false;                                                                       // Player in collider but not in sight 
    }

    bool PlayerHiddenByObstacles()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);         // Get distance to player so that raycast only calculates within this distance
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.position 
                                                    - transform.position, distanceToPlayer);    // Get list of all detected hits between enemy and player

        foreach (RaycastHit2D hit in hits)                                                      // Check each hit until player is found or end of list is reached
        {
            if (hit.transform.tag == "Danger" || hit.transform.tag == "PickUp")                  // Ignore enemies own colliders and other enemies
                continue;
            
            if (hit.transform.tag != "Player")                                                  // Something between player and enemy
                return true;                                                                    // Obstacle between player and enemy
        }
        
        return false;                                                                           // Player is not hidden by an obstacle
    }

    void RunAnimations()
    {
        animations.SetBool("Walking", walking);
    }
}
