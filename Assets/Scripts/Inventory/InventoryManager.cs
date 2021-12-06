using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    //private int coins;

    public TextMeshProUGUI coinText;

    [SerializeField] private ShopItemCollection shop;
    [SerializeField] private GameObject shopDisplayPrefab;
    [SerializeField] private Transform shopParent;

    [SerializeField] private PlayerGamemodeManager playModeM;

    //Extra Varibles to only instatiate once
    private ItemToggle lastReference;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        //Add to SetData Phase
        SaveData.SetData += LoadSaveData;
        //SaveData.instance.data.coins = 5000;
    }

    //Method for SetData Phase
    public void LoadSaveData()
    {
        SaveData.instance.data.coins = 5000;
        DisplayCoins();

        for (int i = 0; i < shop.shopData.Length; i++) //Loop Thought all ShopDataItems
        {
            //Get Temp version of item
            ShopItem tempItem = shop.shopData[i];

            //Create ShopPrefab and set up data!
            GameObject display = Instantiate(shopDisplayPrefab, shopParent);
            display.name = tempItem.Tag;

            lastReference = SaveData.instance.data.FindID(tempItem.Tag);

            //Setup Button
            display.GetComponent<ShopButton>().SetUp(tempItem, lastReference.bought);
            if (lastReference.bought && tempItem.Type == ShopItemType.PlayerMode)
            {
                playModeM.BuyMode(tempItem.Tag);
            }
            
            //If A Item is unknown to the save data, add it!
            if (lastReference.ID == "Null!")
            {
                SaveData.instance.data.shopItems.Add(new ItemToggle(tempItem.Tag, lastReference.bought));
            }
        }
    }

    //Add a coin
    public void AddCoin()
    {
        SaveData.instance.data.coins++;
        DisplayCoins();
    }

    //Get the coin Amount
    public int GetCoins()
    {
        return SaveData.instance.data.coins;
    }

    //Set UI Coin thing to display actual amount
    private void DisplayCoins()
    {
        coinText.text = "Coins: " + SaveData.instance.data.coins;
    }

    //Method for when a shop item is purchased via shop button
    public void BuyItem(int value, string index, ShopItemType type) 
    {
        //Money Change
        SaveData.instance.data.coins -= value;
        DisplayCoins();

        //Claim our prize and store it in the save
        SaveData.instance.data.ChangeItem(index, true, true);
        SaveData.instance.Save();

        //If A player mode, activate it
        if (type == ShopItemType.PlayerMode)
        {
            playModeM.BuyMode(index);
        }
    }

    //Method for toggling varient shop Items
    public void ToggleItem(bool value, string index)
    {
        SaveData.instance.data.ChangeItem(index, true, value);
    }

    //Method that tells other scripts if using a certain modifier
    public bool CheckItemValid(string index)
    {
        //Check if item even exists
        lastReference = SaveData.instance.data.FindID(index);
        if (lastReference.ID != "Null!")
        {
            //Check if willing to do and proud owner of it
            return lastReference.bought && lastReference.toggled;
        }
        else
        {
            //Nice Job it doesn't exist
            Debug.Log("Not Valid Key!");

            return false;
        }
    }

    //Gets Current Player Gamemode
    public PlayerModeData GetCurrentPlayerMode()
    {
        return playModeM.GetCurrentMode();
    }
}

