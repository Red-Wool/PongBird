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

    public void SetUp(ShopItem temp, bool bought)
    {
        //Varible Setup
        index = temp.Tag;

        shopType = temp.Type;
        disappear = shopType == ShopItemType.PlayerMode;

        //Get References
        button = gameObject.GetComponentInChildren<Button>(true);

        //Add Toggle Method for the togglebutton
        toggleButton = gameObject.GetComponentInChildren<Toggle>(true);
        toggleButton.onValueChanged.AddListener(delegate { Toggle(); });

        //Find Objects and set them up
        transform.Find("DescText").GetComponent<TextMeshProUGUI>().text = temp.Info;

        if (!bought) //Only Set up buying if not bought yet
        {
            transform.Find("BuyButton").Find("PriceDisplay").GetComponent<TextMeshProUGUI>().text = temp.Price.ToString();
            value = temp.Price;
        }
        else
        {
            toggleButton.isOn = false; //Activate it! but turn it off 
            ActivateButton(); 
        }
    }

    public void Buy()
    {
        //Only buy if have enough money
        if (InventoryManager.instance.GetCoins() >= value)
        {
            //Buy Item
            InventoryManager.instance.BuyItem(value, index, shopType);

            //Allow Player to toggle or remove the item from shop when bought
            ActivateButton();
        }
    }

    private void ActivateButton() //Send the object to the correct area
    {
        button.gameObject.SetActive(false);
        toggleButton.gameObject.SetActive(true);

        if (disappear)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Toggle();
        }
    }

    //Method for button to Tell Manager the item was toggled
    public void Toggle()
    {
        InventoryManager.instance.ToggleItem(toggleButton.isOn, index);
    }
}
