using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/FlapperMode", order = 5)]
public class FlapperMode : PlayerMode
{
    public override void Action(FishBirdController player)
    {
        if (Input.GetKeyDown(player.savedKey))
        {
            player.pos.y = player.bounceVal;

            player.bounceEffectTimer *= 0.5f;

            player.flapPS.Play();
        }
    }
    public override void Reset(FishBirdController player)
    {
        player.GravityScale(1);
        player.flapPS.Stop();
        var main = player.flapPS.main;
        main.loop = false;
    }
}
