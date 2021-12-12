using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/PsychicMode", order = 5)]
public class PsychicMode : PlayerMode
{
    public override void Action(FishBirdController player)
    {
        throw new System.NotImplementedException();
    }

    public override void Reset(FishBirdController player)
    {
        player.GravityScale(0.1f);
    }


}
