using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float yOffset = 6f;
    [SerializeField] private float xOffset = 8f;

    private float offsetY;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        target = player.transform;
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10);
        offsetY = yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x >= 33.5f && target.position.x <= 39.5f && target.position.y >= -3f)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = 25f;
            transform.position = new Vector3(target.position.x + 10, target.position.y + yOffset, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            offsetY = yOffset;
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            transform.position = new Vector3(target.position.x, target.position.y + offsetY, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = 15f;
            transform.position = new Vector3(target.position.x, target.position.y + offsetY, -10);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        /*
         * Calculate vertical offset for main camera
         */
        var vert = Input.GetAxis("Vertical");

        if (vert < -0.1 && offsetY > -yOffset)
            offsetY -= 0.4f;

        else if (offsetY > yOffset && vert == 0)
            offsetY -= 0.4f;

        else if (offsetY < yOffset && vert == 0)
            offsetY += 0.4f;

    }
}

