using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBirdController : MonoBehaviour
{
    public KeyCode key;

    public float bounceVal;
    public float speed;
    public bool dead;

    public GameManager gm;

    private Vector3 beginPos;

    bool posDirection = true;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = this.transform.position;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            pos = rb.velocity;
            pos.x = speed * (posDirection ? 1 : -1);

            if (Input.GetKeyDown(key))
            {
                pos.y = bounceVal;
            }

            rb.velocity = pos;
        }
        //rb.AddForce(Vector2.right * speed * (posDirection ? 1 : -1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Paddle")
        {
            //Debug.Log("Hit");
            sr.flipX = !sr.flipX;

            gm.HitPaddle(posDirection);

            posDirection = !posDirection;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            gm.GetCoin(1);
            Destroy(collision.gameObject);
        }
    }

    public void Reset()
    {
        dead = false;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        rb.angularVelocity = 0f;

        transform.rotation = Quaternion.identity;
        transform.position = beginPos;

        GetComponent<BoxCollider2D>().enabled = true;

        posDirection = true;
        sr.flipX = false;
    }

    public void LoseGame()
    {
        Debug.Log("Dead");
        rb.freezeRotation = false;
        rb.angularVelocity = 999f;

        GetComponent<BoxCollider2D>().enabled = false;

        dead = true;
        rb.velocity += Vector2.up * rb.velocity * -0.6f;
        rb.velocity += Vector2.up * Random.Range(5f,15f);

        gm.LoseGame();
    }
}
