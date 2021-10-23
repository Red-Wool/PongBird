using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUFO : MonoBehaviour
{
    private float coinTimer;
    private int tempScore;

    public GameObject coinPrefab;
    public Vector2 velocityRange;

    public AnimationCurve xDist;
    public AnimationCurve yDist;

    private float moveTimer;
    private bool direction = false;

    private GameObject tempCoin;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Test");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinTimer += Time.deltaTime * Time.timeScale;
        if (coinTimer >= 0.6f)
        {
            SpawnCoin(tempScore);
            coinTimer %= 0.6f;
        }

        moveTimer += Time.deltaTime * Time.timeScale * 0.2f * (direction ? 1f : -1f);
        if (moveTimer >= 0f && moveTimer <= 1f)
        {
            transform.position = new Vector3(xDist.Evaluate(moveTimer), yDist.Evaluate(moveTimer));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SpawnCoin(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            tempCoin = Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
            tempCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Random.Range(velocityRange.x * -1, velocityRange.x),
                Random.Range(1f, velocityRange.y));
        }
    }

    public void StartSpawn(int score)
    {
        Debug.Log("On the move!");
        direction = !direction;

        moveTimer = direction ? 0f : 1f;

        gameObject.SetActive(true);

        tempScore = score;
    }


}
