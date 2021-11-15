using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerTwoRender;

    [SerializeField] private PlayerManager playerM;
    [SerializeField] private TextMeshProUGUI controlText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerM.PlayingCoop)
        {
            controlText.text = "Player 1\nLeft Paddle: " + ControlManager.instance.CurrentLeftPaddle + "\nAction: " + ControlManager.instance.CurrentPlayerOneAction +
                "\n\nPlayer 2\nRight Paddle: " + ControlManager.instance.CurrentRightPaddle + "\nAction: " + ControlManager.instance.CurrentPlayerTwoAction;
            playerTwoRender.SetActive(true);
        }
        else
        {
            controlText.text = "\nAction: " + ControlManager.instance.CurrentPlayerOneAction +
                "\nLeft Paddle: " + ControlManager.instance.CurrentLeftPaddle + "\nRight Paddle: " + ControlManager.instance.CurrentRightPaddle;
            playerTwoRender.SetActive(false);
        }
    }
}
