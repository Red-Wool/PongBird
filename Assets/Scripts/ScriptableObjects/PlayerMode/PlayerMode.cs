using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMode : ScriptableObject
{
    public abstract void Action(FishBirdController player);
    public abstract void Reset();
};
