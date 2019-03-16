﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    private int maxHealth = 4;                                  // Max player health -> Needs to be equal to number of crystals
    private int currentHealth;                                  // Players current health 
    private bool playerDead;                                    // You died if true (Hahaha you suck)

    private List<GameObject> floatingCrystals;                  // List to hold reference to floating crystals
    private List<GameObject> brokenCrystals;                    // List to hold reference to broken cystals

	// Use this for initialization
	void Awake () {
        currentHealth = maxHealth;
        playerDead = false;

        floatingCrystals = new List<GameObject>();
        brokenCrystals = new List<GameObject>();

        for (int i = 0; i < maxHealth; i++)
        {
            floatingCrystals.Add(this.gameObject.transform.GetChild(3).GetChild(i).gameObject); // Store each gameobject in the list
            brokenCrystals.Add(this.gameObject.transform.GetChild(4).GetChild(i).gameObject);   // Store each gameobject in the list
        }
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;                                // Apply damage value to health

        if (currentHealth <= 0)
            playerDead = true;                                  // Player has died 

        UpdateHealthBar();
    }

    public void ApplyHeal(int heal)
    {
        currentHealth += heal;                                  // Apply heal value to health

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;                          // Player cannot have more than maximum health

        UpdateHealthBar();
    }
	
	// Update is called once per frame
	void UpdateHealthBar () {
        switch (currentHealth) {
            case 4:
                for (int i = 0; i < maxHealth; i++)
                {
                    floatingCrystals[i].SetActive(true);     
                    brokenCrystals[i].SetActive(false);       
                }
                break;
            case 3:
                for (int i = 0; i < maxHealth; i++)
                {
                    if (i >= 3)
                    {
                        floatingCrystals[i].SetActive(false);
                        brokenCrystals[i].SetActive(true);
                    } else
                    {
                        floatingCrystals[i].SetActive(true);
                        brokenCrystals[i].SetActive(false);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < maxHealth; i++)
                {
                    if (i >= 2)
                    {
                        floatingCrystals[i].SetActive(false);
                        brokenCrystals[i].SetActive(true);
                    }
                    else
                    {
                        floatingCrystals[i].SetActive(true);
                        brokenCrystals[i].SetActive(false);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < maxHealth; i++)
                {
                    if (i >= 1)
                    {
                        floatingCrystals[i].SetActive(false);
                        brokenCrystals[i].SetActive(true);
                    }
                    else
                    {
                        floatingCrystals[i].SetActive(true);
                        brokenCrystals[i].SetActive(false);
                    }
                }
                break;
            default:
                for (int i = 0; i < maxHealth; i++)
                {
                        floatingCrystals[i].SetActive(false);
                        brokenCrystals[i].SetActive(true);
                }
                break;
        }
    }
}
