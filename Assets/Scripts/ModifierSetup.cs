using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager), typeof(PlayerManager))]
public class ModifierSetup : MonoBehaviour
{
    private GameManager gameM;
    private PlayerManager playerM;

    [Header("Player Info"), Space(10)]
    private FishBirdController playerOne;
    private FishBirdController playerTwo;

    [Header("Paddle Info"), Space(10), 
     SerializeField] private GameObject leftPaddle;
    [SerializeField] private GameObject rightPaddle;
    [SerializeField] private GameObject paddleBoundery;

    private Vector3 posRef;

    [Header("Drill Escort"), SerializeField, Space(10)] private GameObject escortDrill;

    private bool escortAlive;


    // Start is called before the first frame update
    void Start()
    {
        gameM = GetComponent<GameManager>();
        playerM = GetComponent<PlayerManager>();

        playerOne = playerM.GetPlayer(true).GetComponent<FishBirdController>();
        playerTwo = playerM.GetPlayer(false).GetComponent<FishBirdController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (escortAlive && !escortDrill.activeSelf)
        {
            gameM.LoseGame();
        }
    }

    private void GetPlayers()
    {
        playerM = GetComponent<PlayerManager>();

        ControlManager.instance.playingCoop = playerM.PlayingCoop;

        playerOne = playerM.GetPlayer(true).GetComponent<FishBirdController>();

        playerTwo = playerM.GetPlayer(false).GetComponent<FishBirdController>();
        playerTwo.gameObject.SetActive(playerM.PlayingCoop);
    }

    public void SetUp()
    {
        //SetUp Player
        GetPlayers();

        playerOne.Reset();
        playerTwo.Reset();
        if (InventoryManager.instance.CheckItemValid("HighJump"))
        {
            playerOne.bounceVal = 15f;
            playerTwo.bounceVal = 15f;
        }
        else
        {
            playerOne.bounceVal = 10f;
            playerTwo.bounceVal = 10f;
        }
        playerOne.bounceVal = (InventoryManager.instance.CheckItemValid("HighJump")) ? 15f : 10f; //High Jump

        //Check Drill Escort
        if (InventoryManager.instance.CheckItemValid("DrillEscort"))
        {
            escortDrill.transform.position = Vector3.zero;
            escortDrill.GetComponent<FlyingDrill>().SetUpRocket(true);
            escortAlive = true;
        }
        else
        {
            escortAlive = false;
        }

        //Clamp paddles into bounds and check if Infipaddle Bounds, and sets Paddle Bounce
        leftPaddle.transform.position = ClampPaddle(leftPaddle.transform.position);
        leftPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        leftPaddle.GetComponent<PaddleBounce>().SetBounceVal(InventoryManager.instance.CheckItemValid("PaddleBoost") ? 15 : 10);

        rightPaddle.transform.position = ClampPaddle(rightPaddle.transform.position);
        rightPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        rightPaddle.GetComponent<PaddleBounce>().SetBounceVal(InventoryManager.instance.CheckItemValid("PaddleBoost") ? 15 : 10);


        paddleBoundery.SetActive(!InventoryManager.instance.CheckItemValid("InfipaddleBounds"));
    }

    public Vector3 ClampPaddle(Vector3 pos) 
    {
        pos.y = Mathf.Clamp(pos.y, -6.38f, 6.3f);
        return pos;
    }
}
