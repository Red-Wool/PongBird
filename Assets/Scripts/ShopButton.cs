using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject button;

    private Toggle toggleButton;

    [SerializeField]
    private int value;
    [SerializeField]
    private string index;

    // Start is called before the first frame update
    void Start()
    {
        toggleButton = this.gameObject.GetComponentInChildren<Toggle>(true);
        toggleButton.onValueChanged.AddListener(delegate {Toggle();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy()
    {
        if (InventoryManager.instance.GetCoins() >= value)
        {
            InventoryManager.instance.BuyItem(value, index);

            toggleButton.gameObject.SetActive(true);
            Toggle();

            button.SetActive(false);
        }
    }

    public void Toggle()
    {
        InventoryManager.instance.ToggleItem(toggleButton.isOn, index);
    }
}
