using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Button : MonoBehaviour {

    public GameObject Pausemenu, PauseButton;

    // Use this for initialization
    public void Pause()
    {
        Pausemenu.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public void Resume()
    {
        Pausemenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}
