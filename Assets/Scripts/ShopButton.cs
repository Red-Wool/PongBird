using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Button button;

    [SerializeField]
    private Toggle toggleButton;

    [SerializeField]
    private int value;
    [SerializeField]
    private string index;

    private bool disappear = false;
    private ShopItemType shopType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(string ind, string info, int price, ShopItemType type)
    {
        //Varible Setup
        index = ind;

        disappear = type == ShopItemType.PlayerMode;
        shopType = type;

        //Get References
        button = gameObject.GetComponentInChildren<Button>(true);

        //Add Toggle Method for the togglebutton
        toggleButton = gameObject.GetComponentInChildren<Toggle>(true);
        toggleButton.onValueChanged.AddListener(delegate { Toggle(); });

        //Find Objects and set them up
        transform.Find("DescText").GetComponent<TextMeshProUGUI>().text = info;
        transform.Find("BuyButton").Find("PriceDisplay").GetComponent<TextMeshProUGUI>().text = price.ToString();
        value = price;
    }

    public void Buy()
    {
        //Only buy if have enough money
        if (InventoryManager.instance.GetCoins() >= value)
        {
            //Buy Item
            InventoryManager.instance.BuyItem(value, index, shopType);

            //Allow Player to toggle or remove the item from shop when bought
            toggleButton.gameObject.SetActive(true);
            Toggle();

            button.gameObject.SetActive(false);

            if (disappear) 
            {
                gameObject.SetActive(false);
            }
        }
    }

    //Method for button to Tell Manager the item was toggled
    public void Toggle()
    {
        InventoryManager.instance.ToggleItem(toggleButton.isOn, index);
    }
}
