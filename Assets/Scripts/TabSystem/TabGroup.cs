using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private TabButton primaryButton;

    private List<TabButton> tabs;

    private TabButton selectedButton;

    [SerializeField] private bool changeContent;

    [SerializeField] private Vector2 selectDisplacement;

    [SerializeField] private Color selectColor;
    [SerializeField] private Color idleColor;

    private void Start()
    {
        RestartTab();
    }

    public void Subscribe(TabButton button)
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }

        tabs.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        Debug.Log("Entered Prozimity!");

        ResetTabs();
        button.MoveToward(selectDisplacement);

    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

        if (selectedButton != null && selectedButton != button)
        {
            button.MoveToward(Vector3.zero);
        }
            

    }

    public void OnTabClick(TabButton button)
    {
        selectedButton = button;

        ResetTabs();
        button.MoveToward(selectDisplacement);

        button.background.color = selectColor;
    }

    public void ResetTabs()
    {
        if (selectedButton == null)
        {
            Debug.LogError(name + " Tab Group does not have a Selected Button");
            return;
        }

        selectedButton.MoveToward(selectDisplacement);

        foreach (TabButton button in tabs)
        {
            if (selectedButton != button)
            {
                button.background.color = idleColor;
                button.MoveToward(Vector3.zero);
            }

            if (changeContent)
            {
                button.SetContentActive(selectedButton == button);
            }
        }
    }

    public void RestartTab()
    {
        selectedButton = primaryButton;
        ResetTabs();
    }
}


public struct Tab
{
    public TabButton tab;
    public GameObject content;
}