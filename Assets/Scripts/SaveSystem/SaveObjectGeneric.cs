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

    public ItemToggle FindID(string id)
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (id == shopItems[i].ID)
            {
                return shopItems[i];
            }
        }
        return new ItemToggle("Null!", false);
    }

    public void ChangeItem(string id, bool bought, bool toggle)
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (id == shopItems[i].ID)
            {
                shopItems[i].bought = bought;
                shopItems[i].toggled = toggle;
                return;
            }
        }
        Debug.Log("Invalid Item! " + id);
    }
}
