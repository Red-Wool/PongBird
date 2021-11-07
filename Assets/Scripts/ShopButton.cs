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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(string ind, string info, int price)
    {
        index = ind;

        button = gameObject.GetComponentInChildren<Button>(true);

        toggleButton = gameObject.GetComponentInChildren<Toggle>(true);
        toggleButton.onValueChanged.AddListener(delegate { Toggle(); });

        transform.Find("DescText").GetComponent<TextMeshProUGUI>().text = info;

        transform.Find("BuyButton").Find("PriceDisplay").GetComponent<TextMeshProUGUI>().text = price.ToString();
        value = price;
    }

    public void Buy()
    {
        if (InventoryManager.instance.GetCoins() >= value)
        {
            InventoryManager.instance.BuyItem(value, index);

            toggleButton.gameObject.SetActive(true);
            Toggle();

            button.gameObject.SetActive(false);
        }
    }

    public void Toggle()
    {
        InventoryManager.instance.ToggleItem(toggleButton.isOn, index);
    }
}
