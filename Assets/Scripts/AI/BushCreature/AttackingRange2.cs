using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingRange2 : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIAttacking2>().playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            this.transform.parent.GetComponent<AIAttacking2>().playerInRange = false;
    }
}
