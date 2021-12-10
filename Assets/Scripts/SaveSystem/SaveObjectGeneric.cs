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

public class V3SaveData : V2SaveData
{
    public List<HighscoreSave> highscore;

    public int CheckHighScore(int score, ShopItemCollection shopItems, string charTag)
    {
        string mode = CheckWhichModeActive(shopItems);
        for (int i = 0; i < highscore.Count; i++)
        {
            if (highscore[i].modeTag == mode && highscore[i].characterTag == charTag)
            {
                if (score > highscore[i].score)
                {
                    highscore[i].score = score;
                    return 1;
                }

                return 0;
            }
        }

        highscore.Add(new HighscoreSave(score, mode, charTag));
        return 2;
    }

    private string CheckWhichModeActive(ShopItemCollection shopItems)
    {
        int counter = 0;
        string lastRef = "Classic";

        foreach (ShopItem i in shopItems.shopData)
        {
            if (FindID(i.Tag).toggled)
            {
                counter++;
                lastRef = i.Tag;
            }
        }

        if (counter > 1)
        {
            return counter + " Varients";
        }
        return lastRef;
    }

    public string GetHighScore(string charTag, string modTag)
    {
        foreach (HighscoreSave i in highscore)
        {
            if (i.characterTag == charTag && i.modeTag == modTag)
            {
                return i.score.ToString();
            }
        }
        return "No Record Yet!";
    }
}
