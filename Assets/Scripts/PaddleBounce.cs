using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounce : MonoBehaviour
{
    [Header("Drill Info"), Space(10),
    SerializeField] private KeyCode key; 
    [SerializeField] private float bounceVal;

    [Header("Reference"), SerializeField, Space(10)] private GameManager gm;

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
    
    public void SetBounceVal(float val)
    {
        bounceVal = val;
    }
}
