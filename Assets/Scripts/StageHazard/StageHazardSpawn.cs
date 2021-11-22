using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHazardSpawn : MonoBehaviour
{
    //Pipe Varibles
    [Header("Pipe Info"), Space(10),
     SerializeField] private Vector2 pipeRange;
    [SerializeField] private GameObject[] pipeObjects;

    [SerializeField] private AnimationCurve pipeSize;

    private bool[] pipeEnabled = new bool[3];
    private Vector2[] target = new Vector2[3];
    private float pipeTimer;

    private Vector2 tempPos;

    //Drill Varibles
    [Header("Drill Info"), Space(10),
     SerializeField] private PoolObject rocketPool;
    [SerializeField] private ParticleSystem rocketPS;

    private float rocketTimer;
    private GameObject tempRocket;
    private List<EnemySpawnInfo> rocketSpawnInfo;

    //GoldUFO Varibles
    [Header("GoldUFO Info"), Space(10),
     SerializeField] private GoldUFO goldUFO;

    [Header("Other Info"), Space(10)]
    public bool coop;

    // Start is called before the first frame update
    void Start()
    {
        //Set Up Varibles
        pipeEnabled = new bool[pipeObjects.Length];
        target = new Vector2[pipeObjects.Length];

        rocketSpawnInfo = new List<EnemySpawnInfo>();

        rocketPool.AddPool(10);

        StageHazardSetUp(0, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Need to optimize these pipe things
        //pipeTimer = Time.deltaTime * (coop ? 0.2f : 3f);
        pipeTimer = Time.deltaTime * (coop ? 0.8f : 12f);
        for (int i = 0; i < pipeObjects.Length; i++)
        {
            //Lerp to position randomly selected before
            pipeObjects[i].transform.position = Vector2.Lerp(pipeObjects[i].transform.position, target[i], coop ? 0.004f : 0.2f);

            //Don't change pipe size if it is the same
            if ((pipeObjects[i].transform.localScale.y == pipeSize.Evaluate(1f) && !pipeEnabled[i]) || (pipeObjects[i].transform.localScale.y == pipeSize.Evaluate(0f) && pipeEnabled[i]))
            {
                continue;
            }

            //Stuff to change pipe if on or off
            tempPos = pipeObjects[i].transform.localScale;

            //Debug.Log(tempPos.y + " " + Time.deltaTime * (coop ? 0.8f : 12f) * (pipeEnabled[i] ? -1 : 1));
            tempPos.y = Mathf.Clamp(tempPos.y + (pipeTimer * (pipeEnabled[i] ? -1f : 1f)), 1f, 5f);
            //tempPos.y = pipeSize.Evaluate(tempPos.y - 1);

            /*if (pipeEnabled[i])
            {
                tempPos.y = pipeSize.Evaluate(pipeEnabled[i] ? 1f - pipeTimer : pipeTimer);
            }
            else
            {
                tempPos.y = pipeSize.Evaluate(pipeTimer);
            } */
            pipeObjects[i].transform.localScale = tempPos;

        }

        //Check if Pipe Dream
        if (!InventoryManager.instance.CheckItemValid("PipeDream"))
        {
            //Manage Rockets
            rocketTimer = Time.deltaTime * Time.timeScale;

            //goes through rocket intervals
            for (int i = rocketSpawnInfo.Count - 1; i >= 0; i--)
            {
                rocketSpawnInfo[i].TimePass(rocketTimer); 

                //Send the warning before they come
                if (rocketSpawnInfo[i].Time - 2f < rocketTimer && Mathf.Abs(rocketSpawnInfo[i].Position.x) == 14f)
                {
                    //Debug.Log("ParticleActivate!");
                    rocketPS.transform.position = rocketSpawnInfo[i].Position;
                    rocketPS.Play();

                    rocketSpawnInfo[i].SetX((rocketSpawnInfo[i].Position.x == 14f) ? 19f : -19f);
                }
                else if (rocketSpawnInfo[i].Time < 0) // Launch Rocket when it is time
                {
                    tempRocket = rocketPool.GetObject();
                    tempRocket.transform.position = rocketSpawnInfo[i].Position;
                    tempRocket.GetComponent<FlyingDrill>().SetUpRocket(rocketSpawnInfo[i].Position.x == -19f);

                    rocketSpawnInfo.RemoveAt(i);
                }
            }
        }
    }

    public void StageHazardSetUp(int score, bool direction)
    {
        //Setup Pipes
        SetUpPipe(Random.Range(0f, 1f) < 0.7, direction);
        target[0].x = -13.5f;
        target[1].x = Random.Range(pipeRange.x * -1f, pipeRange.x);
        target[2].x = 13.5f;

        //Setup Drills
        if (!InventoryManager.instance.CheckItemValid("PipeDream"))
        {
            int num = (int)Mathf.Clamp(Mathf.Round(Random.Range(0f, (score == 0) ? -10 : 
                score * ((InventoryManager.instance.CheckItemValid("ThrillTime")) ? 1f : 0.1f) + 1.5f)) - CalculateBoolArray(pipeEnabled) * 0.8f, 0f, 10f * (coop ? 0.6f : 1f));

            //Setup Rockets
            rocketTimer = 0f;

            for (int i = 0; i < num; i++)
            {
                //Add Rocket to SpawnInfo
                rocketSpawnInfo.Add(new EnemySpawnInfo(
                    Random.Range(2f, 5f),
                    new Vector2((Random.Range(0f, 1f) < 0.5f) ? -14f : 14f, Random.Range(pipeRange.y * -1, pipeRange.y))));
            }
        }

        //Setup GoldUFO
        if (score % 6 == 3 || Random.Range(0f, 1f) < 0.1f)
        {
            ActivateGoldUFO(Mathf.Clamp(score / (5 + score / 4), 1, 4));
        }
    }

    //Pipe Setup Method
    private void SetUpPipe(bool enable, bool direction)
    {
        if (!enable)
        {
            return;
        }

        pipeTimer = 0;

        for (int i = 0; i < pipeObjects.Length; i++)
        {
            //Debug.Log(pipeObjects[i]);
            target[i].x = pipeObjects[i].transform.position.x;

            //pipeEnabled[i] = (Random.Range(0f, 1f) < 0.6f);

            if ((i == 0 && !direction) || (i == pipeObjects.Length - 1 && direction))
            {
                continue;
            }

            //Debug.Log(pipeRange.y);
            target[i].y = Random.Range(pipeRange.y * -1f, pipeRange.y);

            pipeEnabled[i] = (Random.Range(0f, 1f) < 0.2f) ? pipeEnabled[i] : !pipeEnabled[i];


        }
    }

    public int CalculateBoolArray(bool[] array)
    {
        int counter = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i])
                counter++;
        }
        return counter;
    }

    public void ActivateGoldUFO(int score)
    {
        goldUFO.StartSpawn(score);
    }
}