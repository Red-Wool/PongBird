using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDisplay : MonoBehaviour
{
    [SerializeField] private Image display;
    [SerializeField] private Image hpBar;

    private bool hearty;

    private FishBirdController character;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hearty)
        {
            hpBar.fillAmount = character.HP / 4f;
        }
    }

    public void SetUp(FishBirdController player, Sprite displayImg, Color col)
    {
        character = player;
        display.sprite = displayImg;

        col.a = 1;
        hpBar.color = col;
    }

    public void Hearty(bool flag)
    {
        hearty = flag;
        //display.gameObject.SetActive(flag);
    }
}
