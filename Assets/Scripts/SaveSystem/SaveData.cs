using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _instance;
    public static SaveData instance { get
        {
            if (_instance == null)
            {
                _instance = new SaveData();
            }

            return _instance;
        } }

    public delegate void SaveDelegate();
    public static event SaveDelegate SetData;
    public static event SaveDelegate DataLoaded;

    public V2SaveData data;

    public void Save()
    {
        SaveManager.Save("FishballKite", data);
    }

    public void Set()
    {
        data = (V2SaveData)SaveManager.Load("FishballKite");
        
        if (data == null)
        {
            data = new V2SaveData();
            //data.controls = new Dictionary<GameControl, KeyCode>();
            data.shopItems = new List<ItemToggle>();
            data.playerModes = new List<ModeToggle>();
        }

        SetData?.Invoke();

    }

    public void Loaded()
    {
        
        DataLoaded?.Invoke();
    }
}
