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
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
