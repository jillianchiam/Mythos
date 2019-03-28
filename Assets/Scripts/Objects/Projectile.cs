using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float activeTime = 10f;
    [SerializeField] private float objectVelocity = 25f;

    private float velocity;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();                                             // Attach rigidbody
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(velocity, 0);                                       // Used to set velocity upon activation
    }

    // When object becomes active start counting down its lifetime
    void OnEnable()
    {
        var direction = GameObject.Find("Player").transform.localScale.x;               // Get the player's current facing direction

        if (direction == 1f)
        {
            velocity = objectVelocity;                                                  // Set the velocity
            transform.localScale = new Vector3(1, 1, 1);                                // Object should be shot to the right
        }
        else if (direction == -1f)
        {
            velocity = -objectVelocity;                                                 // Set the velocity
            transform.localScale = new Vector3(-1, 1, 1);                               // Object should be shot to the left
        }
        
        Invoke("Disable", activeTime);                                                  // Projectile gets auto disabled after certain time period
    }

    // Set object active to false after lifetime is over
    void Disable()
    {
        gameObject.SetActive(false);                                                    // Set to false so that it can be used again in pool
    }

    // Stop any invokes from happening if object gets disabled first
    void OnDisable()
    {
        CancelInvoke();                                                                 // Cancel the invoke if object gets disabled without Disable being called
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")                                             // If it hits anything other than the player
            gameObject.SetActive(false);                                                // Projectile disappears
    }
}