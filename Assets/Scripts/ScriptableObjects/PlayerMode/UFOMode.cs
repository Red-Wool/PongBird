using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/UFOMode", order = 5)]
public class UFOMode : PlayerMode
{
    public float speedMultiplier;
    public float accerleration;

    public Vector2 minMaxVelocity;

    public override void Action(FishBirdController player)
    {
        player.pos.x *= speedMultiplier;

        player.pos.y = Mathf.Clamp(player.GetRb().velocity.y + (player.bounceVal * accerleration * player.reserved[0] * Time.deltaTime), minMaxVelocity.x, minMaxVelocity.y);
        if (Input.GetKeyDown(player.savedKey))
        {
            player.reserved[0] *= -1;
        }
    }

    public override void Reset(FishBirdController player)
    {
        player.GravityScale(0);

        player.reserved = new float[1];
        player.reserved[0] = -1;
    }
}
