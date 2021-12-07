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
};
