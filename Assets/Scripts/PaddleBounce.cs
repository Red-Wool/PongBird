using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounce : MonoBehaviour
{
    [Header("Paddle Info"), Space(10),
     SerializeField] private KeyCode key; 
    [SerializeField] private float bounceVal;

    [SerializeField] private bool isLeft;

    [Header("Reference"), SerializeField, Space(10)] private GameManager gm;

    private Rigidbody2D rb;

    private const float HITBOXSIZE = 3.6f;
    private const float GRAPHICSIZE = 3.2f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SaveData.DataLoaded += SetControl;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            rb.velocity = Vector3.up * bounceVal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            gm.GetCoin(1);
            collision.gameObject.SetActive(false);
        }
    }
    
    public void SetPaddleSize(float mult)
    {
        //X and Y are switched because of rotation
        Vector2 tempSize = Vector2.one * GRAPHICSIZE;
        tempSize.x = GRAPHICSIZE * mult;
        GetComponent<SpriteRenderer>().size = tempSize;

        tempSize = new Vector2(HITBOXSIZE * mult, 0.6f); 
        GetComponent<BoxCollider2D>().size = tempSize;
    }

    public void SetBounceVal(float val)
    {
        bounceVal = val;

        SetControl();
    }

    private void SetControl()
    {
        key = isLeft ? ControlManager.instance.CurrentLeftPaddle : ControlManager.instance.CurrentRightPaddle;
    }
}
