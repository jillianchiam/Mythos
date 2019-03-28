using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingRange1 : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIAttacking1>().playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIAttacking1>().playerInRange = false;
    }
}
