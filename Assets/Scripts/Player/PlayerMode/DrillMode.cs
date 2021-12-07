using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/DrillMode", order = 5)]
public class DrillMode : PlayerMode //Code for our favorite Drill
{
    //Varible Declaration
    public float speedMult;

    public float speedIncrease;
    public float maxVelocity = 10f;

    public override void Action(FishBirdController player)
    {
        //Go Faster!
        player.pos.x *= speedMult;

        if (Input.GetKeyDown(player.savedKey))
        {
            //Set PS to loop
            var main = player.flapPS.main;
            main.loop = true;
            player.flapPS.Play();
        }
        else if (Input.GetKey(player.savedKey))
        {
            //Increase velocity! (Within Bounds)
            player.pos.y = Mathf.Min(player.GetRb().velocity.y + (player.bounceVal * speedIncrease * Time.deltaTime), maxVelocity);

            //Reduce Player KB
            player.bounceEffectTimer *= 0.9f;
        }
        else if (Input.GetKeyUp(player.savedKey))
        {
            //Stop the PS!
            var main = player.flapPS.main;
            main.loop = false;
        }
    }
    public override void Reset(FishBirdController player)
    {
        //Set gravity
        player.GravityScale(1);
    }
}