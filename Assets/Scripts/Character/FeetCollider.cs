using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            this.transform.parent.GetComponent<CharacterMovement>().onGround = true;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            this.transform.parent.GetComponent<CharacterMovement>().onGround = false;
    }
}
