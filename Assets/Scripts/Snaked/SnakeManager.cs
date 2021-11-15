using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public static SnakeManager instance;

    public delegate void SendTime(float time);
    public static event SendTime OnScoreChange;

    //private GameObject minePrefab;
    [SerializeField] private PoolObject minePool;
    [SerializeField] private PoolObject particlePool;

    private float lifeTime;
    public float LifeTime { get { return lifeTime; } }

    private GameObject tempObj;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        minePool.AddPool(30);
        particlePool.AddPool(4);

        //OnScoreChange = 

        //lifeTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMine(Vector3 pos)
    {
        tempObj = minePool.GetObject();

        tempObj.GetComponent<SnakeMine>().SetUp(pos, lifeTime);
    }

    public void UpdateParticle(int score)
    {
        lifeTime = score * 0.05f + 3.5f;
        OnScoreChange?.Invoke(lifeTime);
    }

    public void ResetMines()
    {
        minePool.DisableAll();
    }

    public void AttachPlayer(FishBirdController player, bool flag)
    {
        player.isSnake = flag;

        Transform temp = player.transform.Find(particlePool.CloneName);

        if (temp)
        {
            temp.gameObject.SetActive(flag);
        }
        else if (flag)
        {
            tempObj = particlePool.GetObject();
            tempObj.transform.SetParent(player.transform);
            tempObj.transform.position = player.transform.position;

        }
        
    }
}
