using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopData/Player Mode Collection", fileName = "New Player Mode Collection")]
public class PlayerModeCollection : ScriptableObject
{
    public PlayerModeData[] playerModeData;
}
