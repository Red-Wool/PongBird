using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return new ModeToggle();
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
        return new ModeToggle();
    }

    private void SetUpInventory()
    {
        modeInventory = new List<ModeToggle>();
        for (int i = 0; i < playModeCollection.playerModeData.Length; i++)
        {
            GameObject display = Instantiate(tabPrefab, tabParent);

            if (i == 0)
            {
                display.SetActive(true);
                playModeTab.SetPrimary(display.GetComponent<TabButton>());
            }
            else
            {
                display.SetActive(false);
            }



            new ModeToggle(display, playModeCollection.playerModeData[i], false);
        }
    }

    public PlayerModeData GetCurrentMode()
    {
        if (modeInventory == null)
        {
            SetUpInventory();
        }

        return Find(playModeTab.SelectedButton.gameObject).modeData;
    }
}
