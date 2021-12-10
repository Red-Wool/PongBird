using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/UFOMode", order = 5)]
public class UFOMode : PlayerMode //Code for the speedy UFO
{
    //Varible Declaration
    public float speedMult;
    public float accerleration;

    public Vector2 minMaxVelocity;

    public override void Action(FishBirdController player)
    {
        //Go Faster!
        player.pos.x *= speedMult;

        //Set velocity speeding Up or Down
        player.pos.y = Mathf.Clamp(player.GetRb().velocity.y + (player.bounceVal * accerleration * player.reserved[0] * Time.deltaTime), minMaxVelocity.x, minMaxVelocity.y);

        //If press key, switch gravity!
        if (Input.GetKeyDown(player.savedKey))
        {
            player.flapPS.Play(true);

            player.reserved[0] *= -1;
        }
    }

    public override void Reset(FishBirdController player)
    {
        //Ignore Gravity and stop PS 
        player.GravityScale(0);

        player.flapPS.Stop();
        var main = player.flapPS.main;
        main.loop = false;

        //Reserve a float in the player to tell what direction to go
        try
        {
            if (Mathf.Abs(player.reserved[0]) != 1)
            {
                player.reserved[0] = -1;
            }
        }
        catch
        {
            player.reserved = new float[1];
            player.reserved[0] = -1;
        }
    }
}
