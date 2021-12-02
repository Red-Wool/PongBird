using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles Button Inputs
public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;

    public bool playingCoop;
    public KeyCode CurrentLeftPaddle { get { return playingCoop ? leftPaddleCoop : leftPaddle; } }
    public KeyCode CurrentRightPaddle { get { return playingCoop ? rightPaddleCoop : rightPaddle; } }
    public KeyCode CurrentPlayerOneAction { get { return playingCoop ? playerOneActionCoop : playerAction; } }
    public KeyCode CurrentPlayerTwoAction { get { return playerTwoActionCoop; } }

    [SerializeField] private KeyCode leftPaddle;
    [SerializeField] private KeyCode rightPaddle;
    [SerializeField] private KeyCode playerAction;

    [SerializeField] private KeyCode leftPaddleCoop;
    [SerializeField] private KeyCode rightPaddleCoop;
    [SerializeField] private KeyCode playerOneActionCoop;
    [SerializeField] private KeyCode playerTwoActionCoop;

    

    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    public KeyCode GetKey(GameControl control)
    {
        switch (control)
        {
            case GameControl.Left:
                return leftPaddle;
            case GameControl.Right:
                return rightPaddle;
            case GameControl.Action:
                return playerAction;
            case GameControl.LeftCoop:
                return leftPaddleCoop;
            case GameControl.RightCoop:
                return rightPaddleCoop;
            case GameControl.ActionOne:
                return playerOneActionCoop;
            case GameControl.ActionTwo:
                return playerTwoActionCoop;
            default:
                return leftPaddle;
        }
    }

    public void ControlChange(GameControl control, KeyCode key)
    {
        switch (control)
        {
            case GameControl.Left:
                leftPaddle = key;
                break;
            case GameControl.Right:
                rightPaddle = key;
                break;
            case GameControl.Action:
                playerAction = key;
                break;
            case GameControl.LeftCoop:
                leftPaddleCoop = key;
                break;
            case GameControl.RightCoop:
                rightPaddleCoop = key;
                break;
            case GameControl.ActionOne:
                playerOneActionCoop = key;
                break;
            case GameControl.ActionTwo:
                playerTwoActionCoop = key;
                break;
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