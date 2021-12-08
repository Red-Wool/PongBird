using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/DefenderMode", order = 5)]
public class DefenderMode : PlayerMode //Code for the hotheaded defender
{
    //Varible Declaration
    public float speedMultNorm;
    public float speedMultRam;
    public float oscSpdNorm;
    public float oscSpdRam;

    public float accel;

    public AnimationCurve oscTravel;

    public override void Action(FishBirdController player)
    {
        //Set velocity speeding Up or Down
        //player.pos.y = Mathf.Clamp(player.GetRb().velocity.y + (player.bounceVal * accerleration * player.reserved[0] * Time.deltaTime), minMaxVelocity.x, minMaxVelocity.y);

        if (Input.GetKeyDown(player.savedKey))
        {
            //Particle System looped
            var main = player.flapPS.main;
            main.loop = true;
            player.flapPS.Play();
        }
        else if (Input.GetKey(player.savedKey))
        {
            player.reserved[1] = CalculateRam(player.reserved[1], accel); //Smoothly transition into ram state
        }
        else if (Input.GetKeyUp(player.savedKey))
        {
            //Stop!
            var main = player.flapPS.main;
            main.loop = false;
        }
        else
        {
            player.reserved[1] = CalculateRam(player.reserved[1], -accel); //Smoothly transition out of ram state
        }

        //Sets speed and oscillate the player
        player.pos.x *= Mathf.Lerp(speedMultNorm, speedMultRam, player.reserved[1]);
        player.reserved[0] += Time.deltaTime * Mathf.Lerp(oscSpdNorm, oscSpdRam, player.reserved[1]);

        //Set Player Position
        player.transform.position = player.transform.position + Vector3.up * (oscTravel.Evaluate(player.reserved[0] % 1f) - player.transform.position.y);
    }

    public override void Reset(FishBirdController player)
    {
        //Ignore Gravity and stop PS 
        player.GravityScale(0);

        player.flapPS.Stop();
        var main = player.flapPS.main;
        main.loop = false;

        //Reserve a float in the player for a timer + Accerlation
        player.reserved = new float[2];
        player.reserved[0] = 0.25f; //Timer
        player.reserved[1] = 0f; //Accerlation
    }

    private float CalculateRam(float current, float speed)
    {
        return Mathf.Clamp(current + (Time.deltaTime * speed), 0, 1);
    }
}
