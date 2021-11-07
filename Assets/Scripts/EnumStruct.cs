﻿using System.Collections;
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
    private static string HexConverter(Color c)
    {
        return "#" + ((int)(c.r * 256) - 1).ToString("X2") + ((int)(c.g * 256) - 1).ToString("X2") + ((int)(c.b * 256) - 1).ToString("X2");
    }
    [SerializeField] private string tag; public string Tag { get { return tag; } }

    [SerializeField] private string name; 
    [SerializeField] private Color color;
    [SerializeField] private string desc; 
    public string Info 
    { get { return "<color=" + HexConverter(color) + ">" + name + "</color>\n" + desc; } } //HexConverter(color)
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

