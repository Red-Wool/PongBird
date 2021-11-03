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

    // Update is called once per frame
    void Update()
    {
        
    }
}
