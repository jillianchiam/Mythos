using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Locate : MonoBehaviour {

	public Transform start_loc;

	// Use this for initialization
	void Start () {
		transform.position = start_loc.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
