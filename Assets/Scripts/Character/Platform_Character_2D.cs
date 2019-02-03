using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Platform_Character_2D : MonoBehaviour {

    // Player Movement fields
    [SerializeField] private float speed;
    private float moveHorizontal;
    private Rigidbody2D rb2d;

    // Animation related fields
    private Animator animations;

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
    
    // Ensures that Animation booleans are associated on each update
    void RunAnimations()
    {
        animations.SetFloat("Movement", Mathf.Abs(moveHorizontal));
    }
}
