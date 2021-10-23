using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierSetup : MonoBehaviour
{
    private GameManager gm;
    public GameObject escortDrill;
    private bool escortAlive;

    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject paddleBoundery;
    private Vector3 posRef;
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

        leftPaddle.transform.position = ClampPaddle(leftPaddle.transform.position);
        leftPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        rightPaddle.transform.position = ClampPaddle(rightPaddle.transform.position);
        rightPaddle.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        paddleBoundery.SetActive(!InventoryManager.instance.CheckItemValid("InfipaddleBounds"));
    }

    public Vector3 ClampPaddle(Vector3 pos) 
    {
        pos.y = Mathf.Clamp(pos.y, -6.38f, 6.3f);
        return pos;
    }
}
