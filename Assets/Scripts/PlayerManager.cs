using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool playingCoop; public bool PlayingCoop { get { return playingCoop; } } 
    private bool deadPlayer; //0 is one, 1 is two 

    [SerializeField]
    private GameObject coopPlayerOne;
    [SerializeField]
    private GameObject coopPlayerTwo;

    private float respawnTimer = 0f;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        playingCoop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;

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
    }

    public bool PlayerDefeated(GameObject player)
    {
        if (!playingCoop || respawnTimer > 0)
        {
            Lose();

            return true;
        }

        respawnTimer = 15f;
        deadPlayer = player == coopPlayerTwo;

        return false;
    }

    public void Lose()
    {
        coopPlayerOne.GetComponent<FishBirdController>().DisablePlayer();
        coopPlayerTwo.GetComponent<FishBirdController>().DisablePlayer();
    }
}
