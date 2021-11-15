using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/DrillMode", order = 5)]
public class DrillMode : PlayerMode
{
    //[]
    public float speedMultiplier;

    public float speedIncrease;
    public float maxVelocity = 10f;

    public override void Action(FishBirdController player)
    {
        player.pos.x *= speedMultiplier;

        if (Input.GetKeyDown(player.savedKey))
        {
            var main = player.flapPS.main;
            main.loop = true;
            player.flapPS.Play();
        }
        else if (Input.GetKey(player.savedKey))
        {
            player.pos.y = Mathf.Min(player.GetRb().velocity.y + (player.bounceVal * speedIncrease * Time.deltaTime), maxVelocity);

            player.bounceEffectTimer *= 0.9f;
        }
        else if (Input.GetKeyUp(player.savedKey)){
            var main = player.flapPS.main;
            main.loop = false;
        }
    }
    public override void Reset(FishBirdController player)
    {
        player.GravityScale(1);
    }
}