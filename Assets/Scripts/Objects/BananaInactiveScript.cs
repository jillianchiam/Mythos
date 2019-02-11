using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaInactiveScript : MonoBehaviour {
    [SerializeField] private float activeTime = 1f;
    [SerializeField] private float bananaVelocity = 10f;

    private float velocity;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
            rb2d.velocity = new Vector2(velocity, 0);
    }

    // When object becomes active start counting down its lifetime
    void OnEnable()
    {
        var direction = GameObject.Find("Player").transform.localScale.x;

        if (direction == 1f)
            velocity = bananaVelocity;
        else if (direction == -1f)
            velocity = -bananaVelocity;

        Invoke("Disable", activeTime);
    }

    // Set object active to false after lifetime is over
    void Disable()
    {
        gameObject.SetActive(false);
    }

    // Stop any invokes from happening if object gets disabled first
    void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
            gameObject.SetActive(false);
    }
}
