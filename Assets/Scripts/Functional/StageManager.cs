using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Ally,
    Enemy
}

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }
    
    public List<GameObject> enemies;
    public List<GameObject> allies;

    public Dictionary<Team, Dictionary<int, GameObject>> teamTable;

    private int characterCount = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        
        else
            instance = this;

        // Set the teams
        teamTable = new Dictionary<Team, Dictionary<int, GameObject>>();
        teamTable[Team.Ally] = new Dictionary<int, GameObject>();
        teamTable[Team.Enemy] = new Dictionary<int, GameObject>();

        // Set the characters
        CharacterData characterData;

        foreach (GameObject character in enemies)
        {
            characterData = character.GetComponent<CharacterData>();

            characterData.id = characterCount;
            characterData.team = Team.Enemy;

            teamTable[Team.Enemy][characterData.id] = character;

            characterCount++;
        }

        foreach (GameObject character in allies)
        {
            characterData = character.GetComponent<CharacterData>();

            characterData.id = characterCount;
            characterData.team = Team.Ally;

            teamTable[Team.Ally][characterData.id] = character;

            characterCount++;
        }
    }

    public List<GameObject> GetEnemies(Team team)
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<Team> teamKeys = new List<Team>(teamTable.Keys);

        foreach (Team key in teamKeys)
        {
            if (key != team)
            {
                enemyList.AddRange(teamTable[key].Values);
            }
        }
       
        return enemyList;
    }

    public List<GameObject> GetFriends(Team team)
    {
        List<GameObject> friendList = new List<GameObject>();
        List<Team> teamKeys = new List<Team>(teamTable.Keys);

        foreach (Team key in teamKeys)
        {
            if (key == team)
            {
                friendList.AddRange(teamTable[key].Values);
            }
        }

        return friendList;
    }

    public void RemoveFromTeam(CharacterData character)
    {
        teamTable[character.team].Remove(character.id);
    }
}
