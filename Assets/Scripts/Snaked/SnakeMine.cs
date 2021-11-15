using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class SnakeMine : MonoBehaviour
{
    [SerializeField]
    private Sprite redMine;

    private float timer;
    private float totalLifeTime;
    private const float ACTIVATETIME = 3f;
    private BoxCollider2D boxCollider;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (!boxCollider.enabled && timer > ACTIVATETIME)
        {
            GetComponent<SpriteRenderer>().sprite = redMine;
            GetComponent<Animator>().enabled = false;

            boxCollider.enabled = true;
        }
        else if (0.5f > totalLifeTime - timer)
        {
            transform.localScale = Vector3.one * (totalLifeTime - timer) * 2f;

            if (timer > totalLifeTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }

    public void SetUp(Vector3 pos, float lifetime)
    {
        if (!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        transform.position = pos;
        transform.localScale = Vector3.one;

        boxCollider.enabled = false;
        GetComponent<Animator>().enabled = true;
        gameObject.SetActive(true);

        timer = 0;
        totalLifeTime = lifetime;
    }
}
