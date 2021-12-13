using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/PsychicMode", order = 5)]
public class PsychicMode : PlayerMode
{
    public float teleportDist;
    public float yPower;
    public float yAdd;
    public float xPower;
    public float xDecay;
    public float gravity;

    private GameObject leftPaddle;
    private GameObject rightPaddle;

    public override void Action(FishBirdController player)
    {
        player.pos.x *= player.reserved[0];
        player.reserved[0] = Mathf.Max(player.reserved[0] - (Time.deltaTime * xDecay), 0);

        if (Input.GetKeyDown(player.savedKey))
        {
            player.flapPS.Play(true);
            Vector3 velocity = ((player.Direction ? rightPaddle.transform.position : leftPaddle.transform.position) - player.transform.position).normalized;

            player.transform.position += velocity * teleportDist;
            player.pos.y = (velocity.y * yPower) + yAdd;
            player.reserved[0] = Mathf.Abs(velocity.x) * xPower;
        }
    }

    public override void Reset(FishBirdController player)
    {
        leftPaddle = GameObject.Find("LeftPaddle");
        rightPaddle = GameObject.Find("RightPaddle");

        player.reserved = new float[1];
        player.reserved[0] = Mathf.Clamp(player.pos.x, 0f, xPower);

        player.GravityScale(gravity);
    }


}
