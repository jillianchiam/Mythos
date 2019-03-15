﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLocate : MonoBehaviour {

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    private string from_loc = "Level2";

    // Use this for initialization
    void Start()
    {
        SceneManager.sceneUnloaded += GetLastLoc;

        if (SceneManager.GetActiveScene().name == "EnemyScene" && door2 && PlayerPrefs.GetString("From_Loc") == from_loc)
            transform.position = door2.transform.position;
        else
            transform.position = door1.transform.position;
    }

    private void GetLastLoc(Scene current)
    {
        PlayerPrefs.SetString("From_Loc", current.name);
        Debug.Log(PlayerPrefs.GetString("From_Loc"));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
