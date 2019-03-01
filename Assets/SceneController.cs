using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private string loc;

    // Use this for initialization
    void Start () {
        loc = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetString("From_Location", loc);
    }
}
