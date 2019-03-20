using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {

    public int damageTaken;
    private bool iFrameCurrent;
    private GameObject healthBar;
    private SpriteRenderer sprite;
    private Color originalColor;
    private Color damageColor;
    private int count;

    // Use this for initialization
    void Start () {
        damageTaken = 0;                                                            // No initial damage
        iFrameCurrent = false;                                                      // Can take damage
        healthBar = GameObject.Find("MainCamera").transform.GetChild(0).gameObject; // Get reference to main camera which holds the healthbar UI
        sprite = GetComponent<SpriteRenderer>();                                    // Attach sprite renderer to variable
        originalColor = sprite.color;                                               // Regular sprite color
        damageColor = Color.red;                                                    // Other sprite color
        count = 0;                                                                  
    }
	
	// Update is called once per frame
	void Update () {
        if (damageTaken > 0 && !iFrameCurrent)                                      // Can only take damage when not invincible
        {
            healthBar.GetComponent<HealthManager>().ApplyDamage(damageTaken);       // Call health bar and make it update the shown health via crystals
            iFrameCurrent = true;                                                   // Give player invincibility frames
            sprite.color = damageColor;                                             // Sprite set to show damage taken
            Invoke("InvincibilityTimer", 1.0f);                                     // Give player 1 second of immunity after taking a hit
        }
	}

    void FixedUpdate ()
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
    }

    void InvincibilityTimer ()
    {
        iFrameCurrent = false;                                                      // iFrames over damage can be taken
        sprite.color = originalColor;                                               // Color needs to be original
        count = 0;                                                                  // Reset for next time damage is taken
    }
}
