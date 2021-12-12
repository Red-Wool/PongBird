using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/FlapperMode", order = 5)]
public class FlapperMode : PlayerMode //Code for the classic flappy mode!
{
    public override void Action(FishBirdController player)
    {
        if (Input.GetKeyDown(player.savedKey))
        {
            Flap(player);
        }
    }
    public override void Reset(FishBirdController player)
    {
        //Set Gravity and stop the particle system
        player.GravityScale(1);
        player.flapPS.Stop();

        ActivatePS(player, false);

        if (Input.GetKey(player.savedKey))
        {
            Flap(player);
        }
    }

    private void Flap(FishBirdController player)
    {
        //Go Up!
        player.pos.y = player.bounceVal;

        //Reduce Some Player KB
        player.bounceEffectTimer *= 0.5f;

        //Play PS
        player.flapPS.Play();
    }
}
