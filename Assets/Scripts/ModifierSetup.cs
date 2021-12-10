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
     SerializeField] private PaddleBounce leftPaddle;
    [SerializeField] private PaddleBounce rightPaddle;
    [SerializeField] private GameObject paddleBoundery;

    private Vector3 posRef;

    [Header("Drill Escort"), SerializeField, Space(10)] private GameObject escortDrill;

    [Header("Particle Systems"), SerializeField, Space(10)] private ParticleSystem popPS;

    private bool escortAlive;

    //Extra Varibles to only instatiate once
    private bool tempBool;

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
            //playerM.Lose();
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
        //ResetPlayers
        playerOne.Reset();
        playerTwo.Reset();

        playerOne.SetMode(InventoryManager.instance.GetCurrentPlayerMode(), 0);
        playerTwo.SetMode(InventoryManager.instance.GetCurrentPlayerMode(), 1);

        //Check HighJump
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

        //Check Safety Net
        tempBool = InventoryManager.instance.CheckItemValid("SafetyNet"); 
        playerOne.isNet = tempBool;
        playerTwo.isNet = tempBool;

        //Check Hearty
        tempBool = InventoryManager.instance.CheckItemValid("Hearty"); 
        playerOne.SetUpHP(tempBool);
        playerTwo.SetUpHP(tempBool);

        //Check Snaked and reset as well
        SnakeManager.instance.ResetMines();
        SnakeManager.instance.UpdateParticle(0);

        tempBool = InventoryManager.instance.CheckItemValid("Snaked");
        SnakeManager.instance.AttachPlayer(playerOne, tempBool);
        SnakeManager.instance.AttachPlayer(playerTwo, tempBool);

        //Check Tiny Paddles
        if (InventoryManager.instance.CheckItemValid("TinyPaddle")) 
        {
            leftPaddle.SetPaddleSize(0.6f);
            rightPaddle.SetPaddleSize(0.6f);
        }
        else
        {
            leftPaddle.SetPaddleSize(1f);
            rightPaddle.SetPaddleSize(1f);
        }
        //playerOne.bounceVal = (InventoryManager.instance.CheckItemValid("HighJump")) ? 15f : 10f; //High Jump

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

        //Clamp paddles into bounds
        leftPaddle.transform.position = ClampPaddle(leftPaddle.transform.position);
        leftPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        rightPaddle.transform.position = ClampPaddle(rightPaddle.transform.position);
        rightPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        //Check PaddleBoost
        if (InventoryManager.instance.CheckItemValid("PaddleBoost")) 
        {
            leftPaddle.SetBounceVal(15);
            rightPaddle.SetBounceVal(15);
        }
        else
        {
            leftPaddle.SetBounceVal(10);
            rightPaddle.SetBounceVal(10);
        }

        //Check InfipaddleBounds
        paddleBoundery.SetActive(!InventoryManager.instance.CheckItemValid("InfipaddleBounds"));
    }

    public void ScoreChange(int score, bool direction)
    {
        //Make snake length longer when score increases
        if (InventoryManager.instance.CheckItemValid("Snaked"))
        {
            SnakeManager.instance.UpdateParticle(score);
        }

        //Send a drill when touch the paddle
        if (InventoryManager.instance.CheckItemValid("Betrayal"))
        {
            posRef = direction ? leftPaddle.transform.position : rightPaddle.transform.position;
            popPS.transform.position = posRef;
            popPS.Play();

            posRef.y = Mathf.Clamp(posRef.y, -30f, 30f);
            posRef.x -= Mathf.Clamp(posRef.x, -1.5f, 1.5f); 
            gameM.stageHs.SpawnDrill(posRef, direction);
        }

        if (InventoryManager.instance.CheckItemValid("PongRemix"))
        {
            playerOne.SetMode(InventoryManager.instance.GetRandomPlayerMode(), 0);
            playerTwo.SetMode(InventoryManager.instance.GetRandomPlayerMode(), 1);
        }
    }

    public Vector3 ClampPaddle(Vector3 pos) 
    {
        pos.y = Mathf.Clamp(pos.y, -6.38f, 6.3f);
        return pos;
    }
}
