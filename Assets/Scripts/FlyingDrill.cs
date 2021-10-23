using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrill : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 0f;

    private bool direction;

    // Start is called before the first frame update
    void Start()
    {
        //direction = true;

        //speed = 0f;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (direction ? Vector2.right : Vector2.left) * speed;
    }

    public void SetUpRocket(bool point)
    {
        direction = point;
        GetComponent<SpriteRenderer>().flipX = !direction;

        speed = 7f;

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bounds")
        {
            speed = 0f;
            gameObject.SetActive(false);
        }
        else if (collision.tag == "Paddle")
        {
            direction = !direction;

            GetComponent<SpriteRenderer>().flipX = !direction;
        }
    }
}
