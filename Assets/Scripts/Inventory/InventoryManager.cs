using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private int coins;

    public TextMeshProUGUI coinText;

    [SerializeField] private ShopItemCollection shop;
    [SerializeField] private GameObject shopDisplayPrefab;
    [SerializeField] private Transform shopParent;

    [SerializeField] private PlayerGamemodeManager playModeM;

    //private string[] names = { "HighJump", "PaddleBoost", "SuperFast", "ThrillTime", "InfipaddleBounds", "DrillEscort", "PipeDream", "DrillMode"};

    private List<ItemToggle> inventory;

    //Extra Varibles to only instatiate once
    private int ind;
    private ItemToggle lastReference;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        inventory = new List<ItemToggle>();

        for(int i = 0; i < shop.shopData.Length; i++)
        {
            ShopItem tempItem = shop.shopData[i];

            GameObject display = Instantiate(shopDisplayPrefab, shopParent);
            display.name = tempItem.Tag;

            display.GetComponent<ShopButton>().SetUp(tempItem.Tag, tempItem.Info, tempItem.Price, tempItem.Type);

            if (tempItem.Type == ShopItemType.Varient)
            {
                inventory.Add(new ItemToggle(tempItem.Tag, false));
            }

        }

        coins = 999;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins;
    }

    public int GetCoins()
    {
        return coins;
    }
    public void SetCoins(int val)
    {
        coins = val;
        coinText.text = "Coins: " + coins;
    }

    public int ShopLength()
    {
        return inventory.Count;
    }

    public void BuyItem(int value, string index, ShopItemType type)
    {
        coins -= value;
        ind = FindID(index);
        if (ind >= 0)
        {
            //Debug.Log("Worked!");

            ChangeInventory(ind, true, true);
        }
        else
        {
            Debug.Log("Can't Buy!");
        }
        
        coinText.text = "Coins: " + coins;
    }
    public void ToggleItem(bool value, string index)
    {
        ind = FindID(index);
        if (ind >= 0)
        {
            ChangeInventory(ind, true, value);
        }
        else
        {
            Debug.Log("Can't Toggle!");
        }
    }
    public bool CheckItemValid(string index)
    {
        ind = FindID(index);
        if (ind >= 0)
        {
            //Debug.Log(index + " " + (lastReference.bought + " " + lastReference.toggled));
            return inventory[ind].bought && inventory[ind].toggled;
        }
        else
        {
            Debug.Log("Not Valid Key!");

            return false;
        }
    }

    public List<ItemToggle> GetInventoryList()
    {
        return inventory;
    }

    private void ChangeInventory(int index, bool b, bool t)
    {
        lastReference = inventory[index];
        lastReference.bought = b;
        lastReference.toggled = t;
        inventory[index] = lastReference;
    }

    private int FindID(string index)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == index)
            {
                return i;
            }
        }

        return -1;
    }

    public PlayerModeData GetCurrentPlayerMode()
    {
        return playModeM.GetCurrentMode();
    }
}

