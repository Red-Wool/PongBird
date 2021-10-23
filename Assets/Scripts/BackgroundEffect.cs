using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    public float xSpeed;
    public float xSnap;

    public float ySpeed;
    public float yIntensity;

    float timer;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        temp = this.transform.position;

        temp.x -= xSpeed * Time.deltaTime;
        temp.x %= xSnap;

        temp.y = Mathf.Sin(timer * ySpeed) * yIntensity;

        this.transform.position = temp;
    }
}
