using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUFO : MonoBehaviour
{
    private float coinTimer;
    private int tempScore;

    [Header("GoldUFO Info"), Space(10),
     SerializeField] private Vector2 velocityRange;
    [SerializeField] private AnimationCurve xDist;
    [SerializeField] private AnimationCurve yDist;

    [Header("References"), Space(10),
     SerializeField] private PoolObject coinPool;
    [SerializeField] private ParticleSystem coinPopPS;

    //private List<GameObject> coinPool = new List<GameObject>();

    private float moveTimer;
    private bool direction = false;

    private GameObject tempCoin;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Test");
        gameObject.SetActive(false);

        coinPool.AddPool(20);
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
        coinPopPS.gameObject.transform.position = this.transform.position;
        coinPopPS.Play();

        //Spawn Coins
        for (int i = 0; i < amount; i++)
        {
            tempCoin = coinPool.GetObject();//Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
            tempCoin.transform.position = transform.position;
            tempCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Random.Range(velocityRange.x * -1, velocityRange.x),
                Random.Range(1f, velocityRange.y));
        }
    }

    public void StartSpawn(int score)
    {
        direction = !direction;

        moveTimer = direction ? 0f : 1f;

        gameObject.SetActive(true);

        tempScore = score;
    }
}
