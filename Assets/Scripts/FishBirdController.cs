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

    private bool isInvinicible;
    private GameObject lastPaddleHit;

    private Vector3 bounceDirection;
    private float bounceEffectTimer;

    bool posDirection = true;

    Rigidbody2D rb;
    SpriteRenderer sr;

    public ParticleSystem flapPS;

    private PlayerMode pm;

    //public PlayerMode Mode { set { pm = value; } }

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = this.transform.position;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        pm = PlayerMode.Flapper;

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
                switch (pm)
                {
                    case PlayerMode.Flapper:

                        break;
                    case PlayerMode.Drill:

                        break;
                    case PlayerMode.UFO:

                        break;
                }
                pos.y = bounceVal;

                bounceEffectTimer *= 0.5f;

                flapPS.Play();
            }

            if (bounceEffectTimer > 0)
            {
                bounceEffectTimer -= Time.deltaTime * Time.timeScale * 2;

                bounceDirection.y *= bounceEffectTimer / 2f;

                pos += bounceDirection * (16f * bounceEffectTimer);
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
            if (lastPaddleHit != collision.gameObject)
            {
                lastPaddleHit = collision.gameObject;

                gm.HitPaddle(posDirection);

                sr.flipX = !sr.flipX;
                posDirection = !posDirection;
            }
        }
        else if (collision.transform.tag == "Player")
        {
            bounceDirection = GetDirection(collision.transform.position);
            bounceEffectTimer = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            gm.GetCoin(1);
            collision.gameObject.SetActive(false);
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

        lastPaddleHit = null;

        posDirection = true;
        sr.flipX = false;
    }

    public void SetInvincible(float time)
    {
        isInvinicible = true;
        StartCoroutine("InvincibleCountdown", time);
    }

    private IEnumerable InvincibleCountdown(float time)
    {
        yield return new WaitForSeconds(time);

        isInvinicible = false;
    }

    public void Defeat(bool trueKill)
    {
        if (trueKill || !isInvinicible)
        {
            DisablePlayer();

            gm.PlayerDefeated(this.gameObject);
        }
        Debug.Log("Dead");
        
    }

    public Vector3 GetDirection(Vector3 otherPos)
    {
        Vector3 tempPos = transform.position - otherPos;

        tempPos.y *= 0.1f;

        tempPos /= tempPos.magnitude;

        return tempPos;
    }

    public void DisablePlayer()
    {
        rb.freezeRotation = false;
        rb.angularVelocity = 999f;

        GetComponent<BoxCollider2D>().enabled = false;

        dead = true;
        rb.velocity += Vector2.up * rb.velocity * -0.6f;
        rb.velocity += Vector2.up * Random.Range(5f, 15f);

        lastPaddleHit = null;
    }
}
