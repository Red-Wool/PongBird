using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Team", menuName = "Gameplay/Player Team")]
public class PlayerTeam : ScriptableObject
{
    //Varible Declaration
    [SerializeField] private string teamName; public string TeamName { get; }
    [SerializeField] private List<FishBirdController> players;
    [SerializeField] private int points; public int Point { get; }

    private List<PlayerSpawnInfo> respawnList;

    [SerializeField] private PlayerTeamConfig config;

    //Add Players to team
    public void AddPlayer(FishBirdController player)
    {
        players.Add(player);
    }

    //Clear a team
    public void ClearPlayerTeam()
    {
        players.Clear();
        respawnList.Clear();
    }

    //Tell when player died;
    public bool PlayerDied(FishBirdController player)
    {
        if (player.dead && players.Contains(player)) //Need to check if player already dead not added yet!
        {
            respawnList.Add(new PlayerSpawnInfo(player, config.respawnTime));
        }
        return respawnList.Count == players.Count;
    }

    //Update the respawn times which check which players can respawn
    public void UpdateRespawnTimes(float t)
    {
        if (config.respawnTime >= 0f)
        {
            if (config.queueRespawn)
            {
                while(t < 0f && respawnList.Count > 0)
                {
                    t = CheckRespawnTime(0, t);
                }
            }
            else
            {
                for (int i = respawnList.Count; i >= 0; i--)
                {
                    CheckRespawnTime(i, t);
                }
            }
        }
    }

    //Helper Method to respawn the player if the time is right
    private float CheckRespawnTime(int i, float t)
    {
        float time = respawnList[i].timer - t;
        if (time <= 0f)
        {
            respawnList[i].player.Reset();
            respawnList.RemoveAt(i);
            return Mathf.Abs(time);
        }
        return 1f;
    }
}

//Class to store misc data about a team
[System.Serializable]
public class PlayerTeamConfig
{
    public float respawnTime;
    public bool queueRespawn;
    public bool teamAttack;
    public int startHP;
}