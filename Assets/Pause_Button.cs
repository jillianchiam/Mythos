using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Button : MonoBehaviour {

    public GameObject Pausemenu, PauseButton;
    /*public int totalMenu = 2;
    public float yOffSet = 1f;
    int index = 0;                                          //arrow

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < totalMenu - 1)                    // Move arrow down
            {
                index++;
                Vector2 position = transform.position;
                position.y -= yOffSet;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)                    // Move up
            {
                index--;
                Vector2 position = transform.position;
                position.y += yOffSet;
                transform.position = position;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                Pausemenu.SetActive(false);
                PauseButton.SetActive(true);
                Time.timeScale = 1;
            } else {
                Application.Quit();
            }
        }
    }
    */
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
