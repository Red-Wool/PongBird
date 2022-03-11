using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnInfo
{
    public FishBirdController player;
    public float timer;

    public PlayerSpawnInfo(FishBirdController p, float t)
    {
        player = p;
        timer = t;
    }
}
