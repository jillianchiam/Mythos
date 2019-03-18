using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/* TODO: Try fixing stuff using the ideas or code from CharacterMovement*/

public class CharacterItems : MonoBehaviour
{

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

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!picked)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "PickUp")
                {
                    rb2d = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    rb2d.gravityScale = 0;  // Cancel gravity on object when picked up
                    rb2d.mass = 0;          // Get rid of any mass so it doesnt impact force or velocity physics
                    airborne = false;
                    picked = true;
                    goingDown = false;
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notPicked) && !onVerticalSurface)
            {
                picked = false;

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    airborne = true;
                    rb2d.gravityScale = 5;  // Player has gravity scale of 5 so match that
                    rb2d.mass = 1;          // Reset the objects mass to 1 so that forces now impact it
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0.45f) * throwForce;
                }
            }
        }

        if (airborne)
        {
            if (rb2d.velocity.y < 0)
            {
                rb2d.AddForce(Vector2.up * gravity * (fallMultiplier) * Time.deltaTime);    //adding force when its going up
                goingDown = true;
            }
            else if (rb2d.velocity.y > 0)
            {
                rb2d.AddForce(Vector2.up * gravity * (lowFallMultiplier) * Time.deltaTime); //falls with less floaty-ness
            }
            else if (goingDown && rb2d.velocity.y == 0)                                   // on the ground
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
