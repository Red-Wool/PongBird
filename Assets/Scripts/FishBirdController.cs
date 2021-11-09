using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBirdController : MonoBehaviour
{
    public KeyCode savedKey;

    public float bounceVal;
    public float speed;
    public bool dead;

    public GameManager gm;

    private Vector3 beginPos;

    [SerializeField] private bool playerTwo;

    private int hp;
    [SerializeField] private Image hpBar;

    private bool isSnake;
    [HideInInspector] public bool isNet;

    private bool isInvinicible;
    private GameObject lastPaddleHit;

    private Vector3 bounceDirection;
    [HideInInspector] public float bounceEffectTimer;

    bool posDirection = true;

    Rigidbody2D rb; public Rigidbody2D GetRb() { return rb; }
    SpriteRenderer sr;

    public ParticleSystem flapPS;

    [SerializeField]
    private PlayerMode playerMode;

    //public PlayerMode Mode { set { pm = value; } }

    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = this.transform.position;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //pm = PlayerMode.Flapper;

        SetControl();

        dead = false;
    }

    private void FixedUpdate()
    {
        if (hpBar.gameObject.activeSelf)
        {
            hpBar.fillAmount = hp / 4f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            pos = rb.velocity;
            pos.x = speed * (posDirection ? 1 : -1);

            playerMode.Action(this);
            /*if (Input.GetKeyDown(savedKey))
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
            }*/

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
        else if ((isNet || isInvinicible) && collision.transform.tag == "Net")
        {
            rb.velocity = rb.velocity * (Vector3.one + Vector3.down * 2);
        }
    }

    public void Reset()
    {
        //If Rigidbody is missing, assume all other references are missing just to be safe
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        SetControl();

        if (hp <= 0)
        {
            hp = 1;
        }

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
        rb.velocity = Vector3.up * 7f;

        SetAlpha(0.8f);

        StartCoroutine("InvincibleCountdown", time);
    }

    private IEnumerator InvincibleCountdown(float time)
    {
        //Debug.Log(time / Time.timeScale);

        yield return new WaitForSeconds(time);

        SetAlpha(1f);
        isInvinicible = false;
    }

    private void SetAlpha(float alphaVal)
    {
        Color col = new Color(1f, 1f, 1f, alphaVal);
        sr.color = col;
    }

    public void SetUpHP(bool flag)
    {
        hpBar.gameObject.SetActive(flag);
        hp = flag ? 4 : 1;
    }

    public void Damage(bool trueKill)
    {
        //hp -= 1;
        if (!isInvinicible)
        {
            hp -= 1;
        }

        if (trueKill || hp <= 0)
        {
            DisablePlayer();

            gm.PlayerDefeated(this.gameObject);

            Debug.Log("Dead");
            return;
        }

        if (!isInvinicible)
        {
            SetInvincible(3f);
        }
    }

    public Vector3 GetDirection(Vector3 otherPos)
    {
        Vector3 tempPos = transform.position - otherPos;

        tempPos.y *= 0.1f;

        tempPos /= tempPos.magnitude;

        return tempPos;
    }

    private void SetControl()
    {
        savedKey = playerTwo ? ControlManager.instance.CurrentPlayerTwoAction : ControlManager.instance.CurrentPlayerOneAction;
    }

    public void DisablePlayer()
    {
        rb.freezeRotation = false;
        rb.angularVelocity = 999f;

        hp = 0;

        GetComponent<BoxCollider2D>().enabled = false;

        dead = true;
        rb.velocity += Vector2.up * rb.velocity * -0.6f;
        rb.velocity += Vector2.up * Random.Range(5f, 15f);

        lastPaddleHit = null;
    }
}
