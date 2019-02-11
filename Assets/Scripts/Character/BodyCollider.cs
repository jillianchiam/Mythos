using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        this.transform.parent.GetComponent<CharacterMovement>().onVerticalSurface = true;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        this.transform.parent.GetComponent<CharacterMovement>().onVerticalSurface = false;
    }
}
