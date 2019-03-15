using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransport : MonoBehaviour
{

    private bool isStanding;

    // public Transform target;
    private GameObject player;

    public delegate void OnDoorClick();
    public event OnDoorClick onDoorClick;

    // Use this for initialization
    void Start()
    {
        isStanding = false;
        onDoorClick += ChangeScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStanding)
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W))
            {
                if (onDoorClick != null)
                    onDoorClick();
            }
    }

    private void ChangeScene()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
            SceneManager.LoadSceneAsync("EnemyScene");
        if (SceneManager.GetActiveScene().name == "EnemyScene")
            if (gameObject.tag == "Door1")
                SceneManager.LoadSceneAsync("Test Scene");
            else if (gameObject.tag == "Door2")
                SceneManager.LoadSceneAsync("Level2");
        if (SceneManager.GetActiveScene().name == "Test Scene")
            SceneManager.LoadSceneAsync("EnemyScene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            isStanding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
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
