using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/* TODO: Try fixing stuff using the ideas or code from CharacterMovement*/

public class CharacterItems : MonoBehaviour {

    RaycastHit2D hit;

    public bool onVerticalSurface;

    [SerializeField] private Transform holdpoint;
    [SerializeField] private LayerMask notPicked;

    [SerializeField] private bool picked;

    [SerializeField] private float distance = 2f;
    [SerializeField] private float holdDist = 2f;
    [SerializeField] private float throwForce;
    [SerializeField] private float fallMultiplier = 600f;
    [SerializeField] private float lowFallMultiplier = 300f;
    private float gravity;
    bool airborne;
    bool goingDown;
    private Rigidbody2D rb2d;

    void Start()
    {
        //rb2d = GameObject.Find("Boomerang").GetComponent<Rigidbody2D>();
        airborne = false;
        goingDown = false;
        gravity = Physics2D.gravity.y;
    }
	
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.J))
        {
            if (!picked)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "PickUp")
                {
                    rb2d = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    airborne = false;
                    picked = true;
                    goingDown = false;
                }
                    
                    //setKinematic();
                    //hit.collider.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notPicked) && !onVerticalSurface)
            {
                picked = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    airborne = true;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.45f) * throwForce;
                } 
                   
         
                    //setKinematic();
                    //hit.collider.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }

        if (airborne)
        {
            if(rb2d.velocity.y < 0)
            {
                rb2d.AddForce(Vector2.up * gravity * (fallMultiplier) * Time.deltaTime);
                goingDown = true;
            } else if (rb2d.velocity.y > 0)
            {
                rb2d.AddForce(Vector2.up * gravity * (lowFallMultiplier) * Time.deltaTime);
            } else if (goingDown && rb2d.velocity.y == 0)
            {
                airborne = false;
                goingDown = false;
            }
        }

        if (picked)
            if (onVerticalSurface)
                hit.collider.gameObject.transform.position = new Vector3((-1 * transform.localScale.x * holdDist) + transform.position.x, holdpoint.position.y, 0); // If on a wall should put on back to climb wall
            else
                hit.collider.gameObject.transform.position = holdpoint.position;                                                                                    // Hold it in holdpoint position
    }

    /*void SimulateProjectile()
    {
        float projectile_throw = distance / (Mathf.Sin(2 * transform.localScale.x * Mathf.Deg2Rad) / gravity); // Calculate the velocity needed to throw object to the target
        Projectile.position = holdpoint.position + new Vector3(0, 0.0f, 0);         // Move projectile to the position of throwing object.

    //}

    //void setKinematic()            //prevent picked object from sliding down
    //{
     // if (!picked)
     //   {
     //       rb.isKinematic = false;
     //   } else
     //   {
            rb.isKinematic = true;
     //   }
   // }

    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}

/* Notes to make sure this implementation actually works:
 * 
 * holdpoint: 
 * Under Hierarchy, click Player,
 * Go to GameObject, click "Create Empty Child"
 * change name of GameObject to holdpoint.
 * Place holdpoint centre anywhere on the green line 
 * 
 * PLEASE do not place Boomerang under Player in Hierarchy! Set it as it's own GameObject
 * 
 * Boomerang:
 *  % Tag as PickUp (have to create your own tag)
 * 
 *  % Transform scale X=Y=Z=0.5
 * 
 *  % Add -
 *   a) Box Collider 2D 
 *   b) Rigidbody2D: Body Type should be Dynamic by default. Ensure "Mass : 0.005". Otherwise, object would be too heavy and player will slide.
 *  
 *   
 * Player:
 * Character_Pick Up (script)
 * 
 * Distance at least 1.5
 * drag holdpoint from Hierarchy and place it in Holdpoint variable 
 * Throwforce is how far you want to throw the object : I just set 8 
 *
 *
 *
 */
