using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeParticle : MonoBehaviour
{
    private ParticleSystem snakePS;

    // Start is called before the first frame update
    void Start()
    {
        snakePS = GetComponent<ParticleSystem>();
        SnakeManager.OnScoreChange += UpdateLifeTime;
    }

    private void UpdateLifeTime(float time)
    {
        var main = snakePS.main;
        main.startLifetime = time;
    }
}
