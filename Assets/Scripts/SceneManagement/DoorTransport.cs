﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransport : MonoBehaviour {

	private bool isStanding;

	// Use this for initialization
	void Start () {
		isStanding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isStanding) {
			if (Input.GetMouseButtonDown (0)) {
				if (SceneManager.GetActiveScene ().name == "Level2")
					Application.LoadLevel ("Test Scene");
				if (SceneManager.GetActiveScene ().name == "Test Scene")
					Application.LoadLevel ("Level2");
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