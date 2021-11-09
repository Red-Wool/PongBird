using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/DrillMode", order = 5)]
public class DrillMode : PlayerMode
{
    //[]
    public float speedIncrease;
    public float maxVelocity = 10f;

    public override void Action(FishBirdController player)
    {
        if (Input.GetKeyDown(player.savedKey))
        {
            player.flapPS.loop = true;
            player.flapPS.Play();
        }
        else if (Input.GetKey(player.savedKey))
        {
            player.pos.y = Mathf.Min(player.GetRb().velocity.y + (player.bounceVal * speedIncrease * Time.deltaTime), maxVelocity);

            player.bounceEffectTimer *= 0.9f;
        }
        else if (Input.GetKeyUp(player.savedKey)){
            player.flapPS.loop = false;
        }
    }
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}