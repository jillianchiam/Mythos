using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Character_2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-0.05f, 0, 0);
	}
}
