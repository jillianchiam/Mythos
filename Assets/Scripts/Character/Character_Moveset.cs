using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moveset : MonoBehaviour {
    public Rigidbody2D banana;
    public float bananaSpeed = 10f;

    private Vector2 facingDirection = Vector2.zero;

    // Use this for initialization
    void Start () {
		
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
        var bananaInst = Instantiate(banana, transform.position, Quaternion.Euler(new Vector2(0, 0)));

        if (facingDirection.x < 0)
            bananaInst.velocity = new Vector2(-bananaSpeed, 0);
        else
            bananaInst.velocity = new Vector2(bananaSpeed, 0);
    }
}
