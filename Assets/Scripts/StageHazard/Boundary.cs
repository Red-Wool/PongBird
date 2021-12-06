using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private bool trueKill;
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
            collision.GetComponent<FishBirdController>().Damage(trueKill);
        }
        else if (collision.transform.tag == "Coin" && trueKill)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
