using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGamemodeManager : MonoBehaviour
{
    [SerializeField] private PlayerModeCollection playModeCollection;
    [SerializeField] private TabGroup playModeTab;
    [SerializeField] private Transform tabParent;

    [SerializeField] private GameObject tabPrefab;

    [SerializeField] private List<ModeToggle> modeInventory;

    //[SerializeField] private List<>

    // Start is called before the first frame update
    void Start()
    {
        SetUpInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyMode (string tag)
    {
        ModeToggle toggle = Find(tag);
        if (toggle.selectObject != null)
        {
            toggle.selectObject.SetActive(true);
        }
    }

    private ModeToggle Find (string tag)
    {
        for (int i = 0; i < modeInventory.Count; i++)
        {
            if (modeInventory[i].modeData.Tag == tag)
            {
                return modeInventory[i];
            }
        }

        Debug.LogError("Not Valid Tag!");
        return new ModeToggle(null, playModeCollection.playerModeData[0], false);
    }

    private ModeToggle Find(GameObject lookObject)
    {
        for (int i = 0; i < modeInventory.Count; i++)
        {
            if (modeInventory[i].selectObject == lookObject)
            {
                return modeInventory[i];
            }
        }

        Debug.LogError("Not Valid Object!");
        return new ModeToggle(null, playModeCollection.playerModeData[0], false);
    }

    private void SetUpInventory()
    {
        modeInventory = new List<ModeToggle>();
        for (int i = 0; i < playModeCollection.playerModeData.Length; i++)
        {
            GameObject display = Instantiate(tabPrefab, tabParent);

            playModeTab.Subscribe(display.GetComponent<TabButton>());
            display.gameObject.GetComponentInChildren<TMP_Text>(true).text = playModeCollection.playerModeData[i].Name;
            display.gameObject.transform.Find("Image").GetComponent<Image>().sprite = playModeCollection.playerModeData[i].Sprite;

            if (i == 0)
            {
                display.SetActive(true);
                playModeTab.SetPrimary(display.GetComponent<TabButton>());
            }
            else
            {
                display.SetActive(false);
            }



            modeInventory.Add(new ModeToggle(display, playModeCollection.playerModeData[i], false));
        }

        playModeTab.RestartTab();
    }

    public PlayerModeData GetCurrentMode()
    {
        if (modeInventory == null)
        {
            SetUpInventory();
        }

        return Find(playModeTab.SelectedButton.gameObject).modeData;
    }

    public PlayerModeData GetRandomMode()
    {
        return modeInventory[Random.Range(0, modeInventory.Count)].modeData;
    }
}
