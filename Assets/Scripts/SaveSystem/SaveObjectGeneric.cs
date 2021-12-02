using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObjectGeneric
{

}

[System.Serializable]
public class V2SaveData : SaveObjectGeneric
{
    public int coins;
    public List<ItemToggle> shopItems;
    public List<ModeToggle> playerModes;
    public Dictionary<GameControl, KeyCode> controls;
}
