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

    private string[] names = { "HighJump", "PaddleBoost", "SuperFast", "ThrillTime", "InfipaddleBounds", "DrillEscort", "PipeDream", "DrillMode"};

    private List<ItemToggle> inventory;

    private bool[] shopItems = new bool[8];
    private bool[] shopToggle = new bool[8];

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
            GameObject display = Instantiate(shopDisplayPrefab, shopParent);
            ShopItem tempItem = shop.shopData[i];

            display.GetComponent<ShopButton>().SetUp(tempItem.Tag, tempItem.Info, tempItem.Price);

            inventory.Add(new ItemToggle(tempItem.Tag, false));
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

    public void BuyItem(int value, string index)
    {
        coins -= value;
        ind = FindID(index);
        if (ind >= 0)
        {
            //Debug.Log("Worked!");

            ChangeInventory(ind, true, true);

            //Debug.Log(lastReference.ToString());
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
}

