using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    private float timer; public float Time { get { return timer; } }
    private Vector2 pos; public Vector2 Position { get { return pos; } }

    public EnemySpawnInfo(float time, Vector2 position)
    {
        timer = time;
        pos = position;
    }

    public void TimePass(float time) { Debug.Log("timer"); timer -= time; }
    public void SetX(float num) { pos.x = num; }
    public void SetY(float num) { pos.y = num; }

}