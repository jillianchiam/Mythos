using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
            GameObject.Find("Canvas").GetComponent<PauseButton>().Pause();
            //GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);

    }
}
