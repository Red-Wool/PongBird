using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumStruct : MonoBehaviour
{

}

/*public enum PlayerMode
{
    Flapper,
    Drill,
    UFO
}*/

[System.Serializable]
public struct ShopItem
{
    private static string HexConverter(Color c)
    {
        return "#" + ((int)(c.r * 255)).ToString("X2") + ((int)(c.g * 255)).ToString("X2") + ((int)(c.b * 255)).ToString("X2");
    }
    [SerializeField] private string tag; public string Tag { get { return tag; } }

    [SerializeField] private string name; 
    [SerializeField] private Color color;
    [SerializeField, Multiline] private string desc; 
    public string Info 
    { get { return "<color=" + HexConverter(color) + ">" + name + "</color>\n" + desc.Replace("\\n","\n"); } } //HexConverter(color)
    [SerializeField] private int price; public int Price { get { return price; } }
    [SerializeField] private ShopItemType type; public ShopItemType Type { get { return type; } }
}

[System.Serializable]
public struct PlayerModeData
{
    [SerializeField] private string tag; public string Tag { get { return tag; } }
    [SerializeField] private PlayerMode gameMode; public PlayerMode GameMode { get { return gameMode; } }

    [SerializeField] private RuntimeAnimatorController[] skin; 
    public RuntimeAnimatorController GetSkin(int i)
    {
        if (i < 0 || i >= skin.Length)
        {
            Debug.LogError("Invalid Skin ID! Tag: " + tag + " Index: " + i);
        }

        return skin[Mathf.Clamp(i, 0, skin.Length - 1)];
    }
    [SerializeField] private Sprite displaySprite; public Sprite Sprite { get { return displaySprite; } }
    [SerializeField] private GameObject particlePrefab; public GameObject Particle { get { return particlePrefab; } }
}

public enum ShopItemType
{
    Varient,
    PlayerMode,
    SkinQuestionMarkMaybeWaitNoWhyAreYouHere
}

[System.Serializable]
public struct ItemToggle
{
    public string ID { get; }
    public bool bought;
    public bool toggled;

    //public string getID() { return id; }

    public ItemToggle(string name, bool value)
    {
        ID = name;
        bought = value;
        toggled = value;
    }

    public override string ToString()
    {
        return "ID: " + ID + " Bought: " + bought + " Toggled: " + toggled;
    }
}

public struct ModeToggle
{
    public GameObject selectObject;

    public PlayerModeData modeData;

    public bool bought;

    public ModeToggle(GameObject select, PlayerModeData data, bool buy)
    {
        selectObject = select;
        modeData = data;
        bought = buy;
    }
}

[System.Serializable]
public struct MenuWindow
{
    
}

