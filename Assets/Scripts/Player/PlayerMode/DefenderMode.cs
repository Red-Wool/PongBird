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
            ActivatePSChildren(player, true);
        }
        else if (Input.GetKey(player.savedKey))
        {
            player.reserved[1] = CalculateRam(player.reserved[1], accel); //Smoothly transition into ram state
        }
        else if (Input.GetKeyUp(player.savedKey))
        {
            //Stop!
            ActivatePSChildren(player, false);
        }
        else
        {
            player.reserved[1] = CalculateRam(player.reserved[1], -accel); //Smoothly transition out of ram state
        }

        //Sets speed and oscillate the player
        player.pos.x *= Mathf.Lerp(speedMultNorm, speedMultRam, player.reserved[1]);
        player.reserved[0] += Time.deltaTime * Mathf.Lerp(oscSpdNorm, oscSpdRam, player.reserved[1]);

        player.pos.y = 0f;

        //Set Player Position
        player.transform.position = player.transform.position + Vector3.up * (oscTravel.Evaluate(player.reserved[0] % 1f) - player.transform.position.y);
    }

    public override void Reset(FishBirdController player)
    {
        //Ignore Gravity and stop PS 
        player.GravityScale(0);

        ActivatePSChildren(player, Input.GetKey(player.savedKey));

        //Reserve a float in the player for a timer + Accerlation
        player.reserved = new float[2];
        player.reserved[0] = CalculatePos(player.transform.position.y); //-Mathf.Acos(player.transform.position.y / 8) / (4 * Mathf.PI); //Timer
        player.reserved[1] = 0.1f * Mathf.Abs(player.transform.position.x); //Accerlation
    }

    private float CalculatePos(float yVal)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Mathf.Abs(oscTravel.Evaluate(i) - yVal) <= 0.25f)
            {
                return i;
            }
        }
        return -0; //Negative zero uhoh
    }

    private float CalculateRam(float current, float speed)
    {
        return Mathf.Clamp(current + (Time.deltaTime * speed), 0, 1);
    }

    /*private void ActivatePS(FishBirdController player, bool flag)
    {
        ParticleSystem[] children = player.flapPS.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < children.Length; i++)
        {
            var main = children[i].main;
            main.loop = flag;

            if (flag)
            {
                player.flapPS.Play();
            }
        }
    }*/
}
