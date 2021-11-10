using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public static SnakeManager instance;

    private GameObject minePrefab;
    private List<GameObject> minePool = new List<GameObject>();

    private GameObject tempMine;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject MineAddPool(int count)
    {
        GameObject mine = null;
        for (int i = 0; i < count; i++)
        {
            mine = Instantiate(minePrefab, Vector3.zero, Quaternion.identity);

            mine.SetActive(false);

            minePool.Add(mine);

            //rocket = tempRocket;
        }
        return mine;
    }

    public GameObject GetMine()
    {
        for (int i = 0; i < minePool.Count; i++)
        {
            if (!minePool[i].activeInHierarchy)
            {
                minePool[i].SetActive(true);

                return minePool[i];
            }
        }

        tempMine = MineAddPool(10);
        return tempMine;
    }
}
