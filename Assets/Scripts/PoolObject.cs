using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObject
{
    [SerializeField]
    private GameObject prefab; public string CloneName { get { return prefab.name + "(Clone)"; } }
    private List<GameObject> pool;

    //private GameObject tempGameObj;

    public PoolObject (GameObject prefabObj)
    {
        prefab = prefabObj;
    }

    public GameObject AddPool(int count)
    {
        CheckPool();
        GameObject tempGameObj = null;
        for (int i = 0; i < count; i++)
        {
            tempGameObj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            //Debug.Log(tempGameObj);
            tempGameObj.SetActive(false);

            pool.Add(tempGameObj);

            //rocket = tempRocket;
        }
        return tempGameObj;
    }

    public void DisableAll()
    {
        CheckPool();
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        CheckPool();
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);

                return pool[i];
            }
        }

        GameObject tempGameObj = AddPool(pool.Count * 2 + 1);
        return tempGameObj;
    }

    private void CheckPool()
    {
        if (pool == null)
        {
            pool = new List<GameObject>();
        }
    }
}
