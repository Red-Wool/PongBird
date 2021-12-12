using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An Abstract class that serves a baseline for player modes
public abstract class PlayerMode : ScriptableObject 
{
    //Action that character will do every frame
    public abstract void Action(FishBirdController player);
    //Reset for varibles when starting new game
    public abstract void Reset(FishBirdController player);

    protected void ActivatePS(FishBirdController player, bool flag)
    {
        var main = player.flapPS.main;
        main.loop = flag;

        if (flag)
        {
            player.flapPS.Play();
        }
    }

    protected void ActivatePSChildren(FishBirdController player, bool flag)
    {
        ParticleSystem[] children = player.flapPS.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < children.Length; i++)
        {
            var main = children[i].main;
            main.loop = flag;

            var emi = children[i].emission;
            emi.enabled = flag;

            if (flag)
            {
                player.flapPS.Play();
            }
        }
    }
};
