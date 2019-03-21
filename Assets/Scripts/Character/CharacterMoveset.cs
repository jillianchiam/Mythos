using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveset : MonoBehaviour
{
    [SerializeField] private float xOffset = 3f;
    [SerializeField] private float yOffset = 0.72f;
    [SerializeField] private float cooldown = 0.5f;

    private bool mainProjRefreshing = false;
    private float chargeCount = 0.0f;
    private float chargeTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && chargeCount < chargeTime)               // Count time of button held down
        {
            chargeCount = chargeCount + 1 * Time.deltaTime;                     // Use delta time to properly count seconds
        }
        else if (chargeCount >= chargeTime && Input.GetButtonUp("Fire1"))       // Charge time must be met and button must be released to fire                                                                         // After specified charge time fire the projectile
        {
            FireProjectile();                                                   // Unleash projectile
            chargeCount = 0;                                                    // Reset charge counter
        }
        else if (Input.GetButtonUp("Fire1"))                                    // Button released but not charged enough
        {
            chargeCount = 0;                                                    // Reset charge count
        }
        /*
        if (Input.GetButtonDown("Fire1"))                                                                                   // If key is pressed execute code to fire a projectile
        {
            if (!mainProjRefreshing)
            {
                mainProjRefreshing = true;                                                                                  // Block from firing another projectile immediately
                FireProjectile();                                                                                           // Fire the projectile
                Invoke("MainProjectileRefreshed", cooldown);                                                                // Activate the cooldown so that another can be fired later
            }
        }
        */
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

    void MainProjectileRefreshed()
    {
        mainProjRefreshing = false;                                                                                         // Re enable projectile fire
    }
}