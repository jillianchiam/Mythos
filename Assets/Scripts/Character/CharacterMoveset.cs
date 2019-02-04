using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveset : MonoBehaviour {
    [SerializeField] private float xOffset = 0.59f;
    [SerializeField] private float yOffset = 0.72f;

    void Start () {
        //rb2d = GetComponent<Rigidbody2D>();                                                                               // Can use in other functions if recoil effect on character is desired
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))                                                                                   // If key is pressed execute code to fire a projectile
            FireProjectile();
    }

    void FireProjectile()
    {
        GameObject projectile = Pooler.sharedInstance.GetPooledObject();                                                    // Get one of the objects from the object pool
        if (projectile == null) return;                                                                                     // No objects left to activate so return nothing

        // Fire banana in direction that player is currently facing
        if (transform.localScale.x == -1f)                                                                                  // Player facing left
        {
            projectile.transform.position = new Vector2(transform.position.x - xOffset, transform.position.y + yOffset);    // Set proper spawn location Which is players current with any specified offsets          
        }
        else                                                                                                                // Player facing right
        {
            projectile.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);    // Set proper spawn location Which is players current with any specified offsets
        }

        projectile.SetActive(true);                                                                                         // Set state to active so that projectile appears on screen
    }
}
