using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private TabButton primaryButton;

    private List<TabButton> tabs;

    private TabButton selectedButton; public TabButton SelectedButton { get { return selectedButton;} }

    [SerializeField] private bool restartStartup;
    [SerializeField] private bool changeContent;
    [SerializeField] private bool changeDisplacement;

    [SerializeField] private Vector2 selectDisplacement;

    [SerializeField] private Color selectColor;
    [SerializeField] private Color idleColor;

    private void Awake()
    {
        if (restartStartup) { RestartTab(); }
    }

    public void SetPrimary (TabButton button)
    {
        primaryButton = button;
    }

    public void Subscribe(TabButton button)
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }

        tabs.Add(button);

        button.SetOwner(this);

        ResetTabs();
    }

    public void OnTabEnter(TabButton button)
    {
        //Debug.Log("Entered Prozimity!");

        ResetTabs();
        ButtonMove(button, selectDisplacement);

    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

        if (selectedButton != null && selectedButton != button)
        {
            ButtonMove(button, Vector3.zero);
        }
            

    }

    public void ButtonMove(TabButton button, Vector3 pos)
    {
        if (changeDisplacement)
        {
            button.MoveToward(pos);
        }
        
    }

    public void OnTabClick(TabButton button)
    {
        selectedButton = button;

        ResetTabs();
        ButtonMove(button, selectDisplacement);

        button.Background.color = selectColor;
    }

    public void ResetTabs()
    {
        if (selectedButton == null)
        {
            if (primaryButton == null)
            {
                Debug.LogError(name + " Tab Group does not have a Selected Button");
                return;
            }

            selectedButton = primaryButton;
        }

        ButtonMove(selectedButton, selectDisplacement);

        foreach (TabButton button in tabs)
        {

            if (selectedButton != button)
            {
                button.Background.color = idleColor;
                ButtonMove(button, Vector3.zero);
            }
            else
            {
                button.Background.color = selectColor;
                ButtonMove(button, selectDisplacement);
            }

            if (changeContent)
            {
                button.SetContentActive(selectedButton == button);
            }
        }
    }

    public void RestartTab()
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }

        selectedButton = primaryButton;

        ButtonMove(selectedButton, selectDisplacement);

        ResetTabs();
    }
}


public struct Tab
{
    public TabButton tab;
    public GameObject content;
}