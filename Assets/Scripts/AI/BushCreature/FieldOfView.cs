using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIMovement>().playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIMovement>().playerInRange = false;
    }
}
