using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounce : MonoBehaviour
{
    public KeyCode key;

    public float bounceVal;

    public GameManager gm;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            rb.velocity = Vector3.up * (InventoryManager.instance.CheckItemValid("PaddleBoost") ? 15 : bounceVal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            gm.GetCoin(1);
            Destroy(collision.gameObject);
        }
    }
}
