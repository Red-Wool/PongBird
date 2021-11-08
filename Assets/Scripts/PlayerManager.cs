using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private bool playingCoop; public bool PlayingCoop { get { return playingCoop; } } 
    private bool deadPlayer; //0 is one, 1 is two 

    [SerializeField]
    private GameObject coopPlayerOne;
    [SerializeField]
    private GameObject coopPlayerTwo;

    [SerializeField]
    private Toggle coopToggle;

    [SerializeField] 
    private GameObject respawnObject;
    private TextMeshProUGUI respawnTimerText;

    private float respawnTimer = 0f;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        playingCoop = false;

        respawnTimerText = respawnObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime / Time.timeScale;

            respawnTimerText.text = respawnTimer.ToString((respawnTimer < 10f) ? "0.0" : "#0");

            if (respawnTimer <= 0)
            {
                RespawnPlayer();

                respawnTimer = 0f;
            }

        }


    }

    public GameObject GetLeader()
    {
        return respawnTimer <= 0 ? null : (deadPlayer ? coopPlayerOne : coopPlayerTwo);
    }

    public GameObject GetPlayer(bool value)
    {
        return value ? coopPlayerOne : coopPlayerTwo;
    }

    public void RespawnPlayer()
    {
        GameObject player = deadPlayer ? coopPlayerTwo : coopPlayerOne;
        player.GetComponent<FishBirdController>().Reset();
        player.GetComponent<FishBirdController>().SetInvincible(3f);
        respawnObject.SetActive(false);
    }

    public bool PlayerDefeated(GameObject player)
    {
        if (!playingCoop || respawnTimer > 0)
        {
            Lose();

            return true;
        }

        respawnTimer = 25f;
        respawnObject.SetActive(true);
        deadPlayer = player == coopPlayerTwo;

        return false;
    }

    public void Lose()
    {
        respawnTimer = 0f;
        respawnObject.SetActive(false);

        coopPlayerOne.GetComponent<FishBirdController>().DisablePlayer();
        coopPlayerTwo.GetComponent<FishBirdController>().DisablePlayer();
    }

    public void CoopToggle()
    {
        playingCoop = coopToggle.isOn;
    }
}
