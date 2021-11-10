using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SnakeMine : MonoBehaviour
{
    private float timer;
    private float totalLifeTime;
    private const float ACTIVATETIME = 3f;
    private BoxCollider2D boxCollider;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (!boxCollider.enabled && timer > ACTIVATETIME)
        {
            boxCollider.enabled = true;
        }
        else if (timer > totalLifeTime)
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

        boxCollider.enabled = false;
        gameObject.SetActive(true);

        timer = 0;
        totalLifeTime = lifetime;
    }
}
