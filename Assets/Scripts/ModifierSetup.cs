﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class ModifierSetup : MonoBehaviour
{
    private GameManager gm;

    [Header("Player Info"), SerializeField, Space(10)] private FishBirdController player;

    [Header("Paddle Info"), SerializeField, Space(10)] private GameObject leftPaddle;
    [SerializeField] private GameObject rightPaddle;
    [SerializeField] private GameObject paddleBoundery;

    private Vector3 posRef;

    [Header("Drill Escort"), SerializeField, Space(10)] private GameObject escortDrill;

    private bool escortAlive;


    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (escortAlive && !escortDrill.activeSelf)
        {
            gm.player.LoseGame();
        }
    }

    public void SetUp()
    {
        //SetUp Player
        player.Reset();
        player.bounceVal = (InventoryManager.instance.CheckItemValid("HighJump")) ? 15f : 10f; //High Jump

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
