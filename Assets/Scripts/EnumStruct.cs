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

[System.Serializable]
public struct MenuWindow
{
    
}

