using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacking : MonoBehaviour {

    [SerializeField] private float radius;                                                      // Radius of circle collider

    private GameObject attackRangeObject;                                                       // To hold child gameobject reference
    private CircleCollider2D attackRange;                                                       // CircleCollider -> Is a child of the enemy gameobject

    public bool playerInRange;                                                                  // True if player is in AttackRange collider
    private bool attacking;                                                                     // True if enemy should try to attack

    private Rigidbody2D rb2d;                                                                   // Enemy's rigidbody
    private Animator animations;                                                                // Enemy's animations

    private Transform player;                                                                   // A reference to the player for raycasting
    private Vector3 lineOfSightEnd;                                                             // Enemies line of sight
    private float scaleFactor;                                                                  // Scale of the enemy set in unity (Assumes x, y, z all scaled by same factor)

    // Use this for initialization
    void Start()
    {
        attackRangeObject = this.gameObject.transform.GetChild(1).gameObject;                   // Second child is the AttackRange object
        attackRange = attackRangeObject.GetComponent<CircleCollider2D>();                       // Attach child's collider  to a variable
        rb2d = GetComponent<Rigidbody2D>();                                                     // Get the enemies rigidbody
        animations = GetComponent<Animator>();                                                  // Attach the animations to the player
        attackRange.radius = radius;                                                            // Apply defined radius to FOV Collider
        player = GameObject.Find("Player").transform;                                           // Get a reference to the player transform
        playerInRange = false;                                                                  // Initialize to false since player not in range
        scaleFactor = transform.localScale.x;
    }

    // Update is called once per frame
    void Update () {
        RunAnimations();
    }

    void FixedUpdate()
    {

    }

    void RunAnimations()
    {
        animations.SetBool("Attacking", attacking);
    }
}
