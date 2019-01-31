using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moveset : MonoBehaviour {
    public Rigidbody2D banana;
    public float bananaGravityImpact = 0f;
    public float bananaSpeed = 10f;
    public float xOffset = 0.59f;
    public float yOffset = 0.72f;
    public float deathTime = 1.0f;

    private Vector2 facingDirection = Vector2.zero;

    // Use this for initialization
    void Start () {
        banana.gravityScale = bananaGravityImpact;
        // banana.deathTime = deathTime;
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
