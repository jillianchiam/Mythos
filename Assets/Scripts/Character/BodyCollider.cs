using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            this.transform.parent.GetComponent<CharacterMovement>().onVerticalSurface = true;
            this.transform.parent.GetComponent<CharacterItems>().onVerticalSurface = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            this.transform.parent.GetComponent<CharacterMovement>().onVerticalSurface = false;
            this.transform.parent.GetComponent<CharacterItems>().onVerticalSurface = false;
        }
    }
}
