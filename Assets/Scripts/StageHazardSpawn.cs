using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHazardSpawn : MonoBehaviour
{
    public GameObject pipe;

    public Vector2 pipeRange;
    public GameObject[] pipeObjects;


    private bool[] pipeEnabled;
    private Vector2[] target;
    private float pipeTimer;
    public AnimationCurve pipeSize;

    private Vector2 tempPos;

    private float rocketTimer;
    public GameObject rocketPrefab;
    private GameObject tempRocket;
    private List<GameObject> rocketPool;
    private Vector2[] rocketPositions;
    private float[] rocketIntervals = { 999f, 999f, 999f, 999f, 999f, 999f, 999f, 999f, 999f, 999f};//10

    public ParticleSystem rocketPS;

    public GameObject protectDrill;

    // Start is called before the first frame update
    void Start()
    {
        pipeEnabled = new bool[pipeObjects.Length];
        target = new Vector2[pipeObjects.Length];

        rocketPool = new List<GameObject>();
        rocketPositions = new Vector2[10];

        RocketAddPool(10);

        StageHazardSetUp(0, true);
        //StageHazardSetUp(0, true);
    }

    // Update is called once per frame
    void Update()
    {
        pipeTimer += Time.deltaTime * 3f;
        for (int i = 0; i < pipeObjects.Length; i++)
        {
            pipeObjects[i].transform.position = Vector2.Lerp(pipeObjects[i].transform.position, target[i], 0.2f);

            if ((pipeObjects[i].transform.localScale.y == pipeSize.Evaluate(1f) && !pipeEnabled[i]) || (pipeObjects[i].transform.localScale.y == pipeSize.Evaluate(0f) && pipeEnabled[i]))
            {
                continue;
            }

            tempPos = pipeObjects[i].transform.localScale;

            if (pipeEnabled[i])
            {
                tempPos.y = pipeSize.Evaluate(1f - pipeTimer);
            }
            else
            {
                tempPos.y = pipeSize.Evaluate(pipeTimer);
            }
            pipeObjects[i].transform.localScale = tempPos;

        }

        if (!InventoryManager.instance.CheckItemValid("PipeDream"))
        {
            rocketTimer += Time.deltaTime * Time.timeScale;
            for (int i = 0; i < rocketIntervals.Length; i++)
            {
                if (rocketIntervals[i] - 2f < rocketTimer && Mathf.Abs(rocketPositions[i].x) == 14f)
                {
                    //Debug.Log("ParticleActivate!");
                    rocketPS.transform.position = rocketPositions[i];
                    rocketPS.Play();

                    rocketPositions[i].x = (rocketPositions[i].x == 14f) ? 19f : -19f;
                }
                else if (rocketIntervals[i] < rocketTimer)
                {
                    tempRocket = GetRocket();
                    tempRocket.transform.position = rocketPositions[i];
                    tempRocket.GetComponent<FlyingDrill>().SetUpRocket((rocketPositions[i].x == -19f));

                    rocketIntervals[i] = 999f;
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

        
        if (!InventoryManager.instance.CheckItemValid("PipeDream"))
        {
            int num = (int)Mathf.Clamp(Mathf.Round(Random.Range(0f, (score == 0) ? -10 : 
                score * ((InventoryManager.instance.CheckItemValid("ThrillTime")) ? 1f : 0.05f) + 1.5f)) - CalculateBoolArray(pipeEnabled) * 0.8f, 0f, 10f);

            //Setup Rockets
            rocketTimer = 0f;

            for (int i = 0; i < num; i++)
            {
                rocketIntervals[i] = Random.Range(2f, 5f);
                rocketPositions[i] = new Vector2((Random.Range(0f, 1f) < 0.5f) ? -14f : 14f, Random.Range(pipeRange.y * -1, pipeRange.y));
            }
        }
    }

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

    public GameObject RocketAddPool(int count)
    {
        GameObject rocket = null;
        for (int i = 0; i < count; i++)
        {
            rocket = Instantiate(rocketPrefab, Vector3.zero, Quaternion.identity);
            //rocket = 
            rocket.SetActive(false);
            //Debug.Log(rocket);
            rocketPool.Add(rocket);

            //rocket = tempRocket;
        }
        return rocket;
    }

    public GameObject GetRocket()
    {
        for (int i = 0; i < rocketPool.Count; i++)
        {
            if (!rocketPool[i].activeInHierarchy)
            {
                rocketPool[i].SetActive(true);
                
                return rocketPool[i];
            }
        }
        
        tempRocket = RocketAddPool(10);
        return tempRocket;
    }
}
