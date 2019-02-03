using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moveset : MonoBehaviour {
    [SerializeField] private Rigidbody2D banana;
    [SerializeField] private float bananaGravityImpact = 0f;
    [SerializeField] private float bananaSpeed = 10f;
    [SerializeField] private float xOffset = 0.59f;
    [SerializeField] private float yOffset = 0.72f;
    [SerializeField] private float deathTime = 1.0f;

    private Vector2 facingDirection = Vector2.zero;

    // Use this for initialization
    void Start () {
        banana.gravityScale = bananaGravityImpact;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            FireBanana();
    }

    void FixedUpdate()
    {
        Vector2 directionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Update the player direction when movement is present
        if (directionVector != Vector2.zero)
        {
            facingDirection = directionVector;
            facingDirection.Normalize();
        }
    }

    void FireBanana()
    {
        // Fire banana in direction that player is currently facing
        if (facingDirection.x < 0)
        {
            var bananaInst = Instantiate(banana, new Vector2(transform.position.x - xOffset, transform.position.y + yOffset), Quaternion.Euler(new Vector2(0, 0)));
            bananaInst.velocity = new Vector2(-bananaSpeed, 0);
        }
        else
        {
            var bananaInst = Instantiate(banana, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.Euler(new Vector2(0, 0)));
            bananaInst.velocity = new Vector2(bananaSpeed, 0);
        }
            
    }
}
