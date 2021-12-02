using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    private static SaveData _instance;
    private static SaveData instance { get
        {
            if (_instance == null)
            {
                _instance = new SaveData();
            }

            return _instance;
        } }

    public SaveObjectGeneric data;
}
