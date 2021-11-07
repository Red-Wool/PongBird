using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumStruct : MonoBehaviour
{

}

public enum PlayerMode
{
    Flapper,
    Drill,
    UFO
}

[System.Serializable]
public struct ShopItem
{
    [SerializeField] private string name; public string Name { get { return name; } }
    [SerializeField] private string desc; public string Desc { get { return desc; } }
    [SerializeField] private int price; public int Price { get { return price; } }
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

