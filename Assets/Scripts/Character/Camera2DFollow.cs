using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float yOffset = 7f;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10);
    }

    // Update is called once per frame
    void Update () {
        if (target.position.x >= 35f && target.position.x <= 42f && target.position.y >= -3f)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = 25f;
            transform.position = new Vector3(target.position.x + 10, target.position.y + yOffset, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            transform.position = new Vector3(target.position.x, target.position.y - 6, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = 10f;
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

