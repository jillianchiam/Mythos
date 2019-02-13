using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float yOffset = 0f;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10);
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, -10);
    }
}

