using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransport : MonoBehaviour {

	private bool isStanding;

    [SerializeField] private Transform target;

	// Use this for initialization
	void Start () {
		isStanding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isStanding) {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W)) {
				if (SceneManager.GetActiveScene().name == "Test Scene")
                {
                    //target.position = new Vector3(-11.7f, -12.7f, 0);           // IDEA 2
                    //DontDestroyOnLoad(target.gameObject);                       // IDEA 2
                    SceneManager.LoadSceneAsync("EnemyScene");
                }	
				if (SceneManager.GetActiveScene().name == "EnemyScene")
                {
                    //target.position = new Vector3(20.07f, -2.35f, 0);           // IDEA 2
                    //DontDestroyOnLoad(target.gameObject);                       // IDEA 2
                    SceneManager.LoadSceneAsync("Test Scene");
                }
            }
		}
	}
		
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			isStanding = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		isStanding = false;
	}
}


/*
 * IDEA 1
 * To move player to specific location
 * Create a master GameObject for the doors
 * Give each door an ID that corresponds to its target's location
 * use the DontDestroyOnLoad function to keep master door active at all times
 * Will simply use a script to set player location to specefied point during scene load
 * 
 * IDEA 2
 * Idea for moving player to specific location 
 * Use DontDestroyOnLoad(Player object)
 * Set new coordinates
 * This creates undesired clones :(
 */