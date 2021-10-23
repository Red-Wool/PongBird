﻿//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;

    public int coins;
    private int nextSpeedUp = 5;

    private float currentSpeed = 1f;

    public StageHazardSpawn shs;
    public ModifierSetup ms;
    public FishBirdController player;

    [SerializeField]
    private TextMeshProUGUI scoreboard;

    [SerializeField]
    private RectTransform speedUpText;
    public AnimationCurve speedUpTextCurve;

    [SerializeField]
    private GameObject retryMenu;
    [SerializeField]
    private GameObject shopMenu;
    [SerializeField]
    private GameObject ieMenu;
    [SerializeField]
    private TMP_InputField ieInputField;

    public RuntimeAnimatorController skin;

    public AudioSource music;

    private float speedUpTextTimer;

    private int highScore;

    // Start is called before the first frame update
    void Start()
    {
        Retry();

        Time.timeScale = 0f;
        /*score = 0;

        speedUpTextTimer = 1f;

        UpdateScore(0);*/
    }

    // Update is called once per frame
    void Update()
    {
        speedUpTextTimer += Time.deltaTime / 5;
        speedUpText.position = Vector3.up * speedUpTextCurve.Evaluate(speedUpTextTimer);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void HitPaddle(bool direction)
    {
        UpdateScore(1);

        shs.StageHazardSetUp(score, direction);

        if (score % 6 == 0 || Random.Range(0f, 1f) < 0.1f)
        {
            InventoryManager.instance.ActivateGoldUFO(Mathf.Clamp(score / (5 + score / 4), 1, 4));
        }

        CalculateSpeed();
    }
    public void UpdateScore(int val)
    {
        score += val;

        scoreboard.text = "Score: " + score;
    }

    public void GetCoin(int val)
    {
        InventoryManager.instance.AddCoin();
    }

    private void CalculateSpeed()
    {
        if (score == nextSpeedUp && currentSpeed < 2.4f)
        {
            currentSpeed += 0.1f;

            Time.timeScale = currentSpeed;

            if (currentSpeed != 2.4f)
                nextSpeedUp += InventoryManager.instance.CheckItemValid("SuperFast") ? 3 : (int)Mathf.Round(5f * currentSpeed); //(int)Mathf.Round(5f * currentSpeed);

            speedUpTextTimer = 0f;

            music.pitch = currentSpeed;
            //speedUpTextTimer
        }
    }

    public void Retry()
    {
        retryMenu.SetActive(false);
        shopMenu.SetActive(false);

        player.Reset();
        player.bounceVal = (InventoryManager.instance.CheckItemValid("HighJump")) ? 15f : 10f;

        score = 0;

        nextSpeedUp = 5;

        if (InventoryManager.instance.CheckItemValid("DrillMode"))
        {
            player.GetComponent<Animator>().runtimeAnimatorController = skin;
        }

        speedUpTextTimer = 1f;

        if (InventoryManager.instance.CheckItemValid("SuperFast"))
        {
            nextSpeedUp = 0;
            currentSpeed = 1.4f;
        }
        
        CalculateSpeed();

        ms.SetUp();

        UpdateScore(0);

        //shs.StageHazardSetUp(0, true);

        music.Play();
    }

    public void Shop()
    {
        retryMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void OpenIEMenu()
    {
        shopMenu.SetActive(false);

        GetGameData();
        ieMenu.SetActive(true);
    }

    public void CloseIEMenu()
    {
        shopMenu.SetActive(true);
        ieMenu.SetActive(false);
    }

    public void CopyClipboard()
    {
        TextEditor td = new TextEditor();
        td.text = ieInputField.text;
        td.SelectAll();
        td.Copy();
    }

    public void PasteClipboard()
    {
        TextEditor td = new TextEditor();
        td.Paste();
        ieInputField.text = td.text;
    }

    //Ignore these
    public void GetGameData()
    {
        string result = "";
        string[] data = new string[6];
        string[] letters = { "A", "B", "C", "D", "E", "F" };
        int[] numbers = { 0, 1, 2, 3, 4, 5 };

        for (int i = 0; i < 6; i++)
        {
            int a = numbers[i];
            int b = Random.Range(0, 5);
            numbers[i] = numbers[b];
            numbers[b] = a;
        }

        string temp = (InventoryManager.instance.GetCoins() % 1000000).ToString("D6");

        data[0] = StringReverse(int.Parse(temp.Substring(0, 5)).ToString("X5"));
        data[1] += temp.Substring(5);

        List<ItemToggle> inv = InventoryManager.instance.GetInventoryList();
        Debug.Log(inv.Count * 2);
        for (int i = 0; i < inv.Count * 2; i++)
        {
            if (i % 2 == 0)
            {
                temp += inv[i / 2].bought ? 1 : 0;
            }
            else
            {
                temp += inv[i / 2].toggled ? 1 : 0;
            }
        }
        data[1] += temp.Substring(6, 4);
        data[1] = int.Parse(data[1]).ToString("X5");
        for (int i = 2; i < 6; i++)
        {
            data[i] += int.Parse(temp.Substring((i - 2) * 4 + 10, 4)).ToString("X3");
        }


        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }

        for (int i = 0; i < data.Length; i++)
        {
            result += letters[numbers[i]] + data[numbers[i]];
        }

        ieInputField.text = result;
    }

    public void TransferGameData()
    {
        TransferGameData(ieInputField.text);
    }

    public void TransferGameData(string data)
    {
        try
        {
            string[] splitResult = new string[6];
            string finalResult = "";
            string letters = "ABCDEF";
            int counter = 0;

            data.Trim();
            
            for (int i = 0; i < 6; i++)
            {
                int index = letters.IndexOf(data[counter]);

                if (index == 0)
                {
                    splitResult[index] = int.Parse(StringReverse(data.Substring(counter + 1, 5)), System.Globalization.NumberStyles.HexNumber).ToString("D4");
                }
                else
                {
                    splitResult[index] = int.Parse(data.Substring(counter + 1, (index > 1) ? 3 : 5), System.Globalization.NumberStyles.HexNumber).ToString("D4");
                }
                
                counter += (index > 1) ? 4 : 6;
            }

            finalResult = "";
            for (int i = 0; i < 6; i++)
            {
                finalResult += splitResult[i];
            }

            InventoryManager.instance.SetCoins(int.Parse(finalResult.Substring(0, 6)));
        }
        catch
        {
            TransferGameData("A00000B00000C000D000E000F000");
        }
    }

    public static string StringReverse(string s)
    {
        if (s.Length > 0)
            return s[s.Length - 1] + StringReverse(s.Substring(0, s.Length - 1));
        else
            return s;
    }

    public void LoseGame()
    {
        currentSpeed = 1f;
        Time.timeScale = currentSpeed;
        music.pitch = currentSpeed;

        retryMenu.SetActive(true);
    }
}