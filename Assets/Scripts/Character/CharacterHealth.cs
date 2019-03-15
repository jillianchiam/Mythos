using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {

    public int damageTaken;
    private bool iFrameCurrent;
    private GameObject healthBar;

	// Use this for initialization
	void Start () {
        damageTaken = 0;
        iFrameCurrent = false;
        healthBar = GameObject.Find("MainCamera").transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (damageTaken > 0 && !iFrameCurrent)
        {
            healthBar.GetComponent<HealthManager>().ApplyDamage(damageTaken);
            iFrameCurrent = true;
            Invoke("InvincibilityTimer", 1.0f);
        }
	}

    void InvincibilityTimer ()
    {
        iFrameCurrent = false;
    }
}
