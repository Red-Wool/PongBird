using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject text;

    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(true);
        Time.timeScale = 0f;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag && Input.anyKeyDown)
        {
            text.SetActive(false);
            Time.timeScale = 1f;

            flag = false;
        }
    }
}
