using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles Button Inputs
public class ControlManager : MonoBehaviour
{
    private static ControlManager _instance;
    public static ControlManager instance{ get
        {
            if (_instance == null)
            {
                _instance = new ControlManager();
            }

            return _instance;
        } }

    public bool playingCoop;
    public KeyCode CurrentLeftPaddle { get { return playingCoop ? SaveData.instance.data.controls[GameControl.LeftCoop] : SaveData.instance.data.controls[GameControl.Left]; } }
    public KeyCode CurrentRightPaddle { get { return playingCoop ? SaveData.instance.data.controls[GameControl.RightCoop] : SaveData.instance.data.controls[GameControl.Right]; } }
    public KeyCode CurrentPlayerOneAction { get { return playingCoop ? SaveData.instance.data.controls[GameControl.ActionOne] : SaveData.instance.data.controls[GameControl.Action]; } }
    public KeyCode CurrentPlayerTwoAction { get { return SaveData.instance.data.controls[GameControl.ActionTwo]; } }

    [SerializeField] private DefaultControl[] defaultControls;
    /*[SerializeField] private DefaultControl rightPaddle;
    [SerializeField] private DefaultControl playerAction;

    [SerializeField] private DefaultControl leftPaddleCoop;
    [SerializeField] private DefaultControl rightPaddleCoop;
    [SerializeField] private DefaultControl playerOneActionCoop;
    [SerializeField] private DefaultControl playerTwoActionCoop;*/

    

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        SaveData.SetData += LoadSaveData;
    }

    private void LoadSaveData()
    {
        if (SaveData.instance.data.controls == null || SaveData.instance.data.controls.Count == 0)
        {
            Dictionary<GameControl, KeyCode> dict = new Dictionary<GameControl, KeyCode>();
            for (int i = 0; i < defaultControls.Length; i++)
            {
                dict.Add(defaultControls[i].control, defaultControls[i].key);
            }
            SaveData.instance.data.controls = dict;
        }
    }

    public KeyCode GetKey(GameControl control)
    {
        return SaveData.instance.data.controls[control];
    }

    public void ControlChange(GameControl control, KeyCode key)
    {
        SaveData.instance.data.controls[control] = key;

        SaveData.instance.Save();
    }

    public void ControlDefault(GameControl control)
    {
        for (int i = 0; i < defaultControls.Length; i++)
        {
            if (control == defaultControls[i].control)
            {
                ControlChange(control, defaultControls[i].key);
            }
        }
    }

    public Dictionary<GameControl, KeyCode> GetAllControls()
    {
        Dictionary<GameControl, KeyCode> cDict = new Dictionary<GameControl, KeyCode>();

        foreach(GameControl control in System.Enum.GetValues(typeof(GameControl)))
        {
            cDict.Add(control, GetKey(control));
        }

        return cDict;
    }

    public void SetAllControls(Dictionary<GameControl, KeyCode> cDict)
    {
        foreach (GameControl control in System.Enum.GetValues(typeof(GameControl)))
        {
            ControlChange(control, cDict[control]);
        }
    }
}

[System.Serializable]
public enum GameControl
{
    Left,
    Right,
    Action,
    LeftCoop,
    RightCoop,
    ActionOne,
    ActionTwo,
}

[System.Serializable]
public struct DefaultControl
{
    [SerializeField] private string name;
    public GameControl control;
    public KeyCode key;
}