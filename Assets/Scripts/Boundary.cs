using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private bool killCoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<FishBirdController>().LoseGame();
        }
        else if (collision.transform.tag == "Coin" && killCoin)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
