using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMode/DefenderMode", order = 5)]
public class DefenderMode : PlayerMode //Code for the hotheaded defender
{
    //Varible Declaration
    public float speedMultNorm;
    public float speedMultRam;
    public float accerlNorm;
    public float accerlRam;

    public AnimationCurve oscTravel;

    public override void Action(FishBirdController player)
    {
        //Set velocity speeding Up or Down
        //player.pos.y = Mathf.Clamp(player.GetRb().velocity.y + (player.bounceVal * accerleration * player.reserved[0] * Time.deltaTime), minMaxVelocity.x, minMaxVelocity.y);

        if (Input.GetKeyDown(player.savedKey))
        {
            var main = player.flapPS.main;
            main.loop = true;
            player.flapPS.Play();
        }
        else if (Input.GetKey(player.savedKey))
        {
            //Go Faster when pressing the key
            player.pos.x *= speedMultRam;

            //Make Player slowly drift up and down
            player.reserved[0] += Time.deltaTime * accerlRam;
        }
        else if (Input.GetKeyUp(player.savedKey))
        {
            var main = player.flapPS.main;
            main.loop = false;
        }
        else
        {
            //Go Slower and oscillate the player rapidly
            player.pos.x *= speedMultNorm;
            player.reserved[0] += Time.deltaTime * accerlNorm;
        }

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

        //Reserve a float in the player for a timer
        player.reserved = new float[1];
        player.reserved[0] = 0;
    }
}
