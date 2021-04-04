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

    public GameObject playerCharacter;

    public Dictionary<Team, Dictionary<int, GameObject>> teamTable;

    private int characterCount = 0;

    [SerializeField] private List<SpawnPoint> spawnPoints;

    [SerializeField] private Mission stageMission;

    public bool stageClear;
    public bool stageFail;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        
        else
            instance = this;

        CreateTeams();
        SpawnCharacters();
    }

    public void CreateTeams()
    {
        // Set the teams
        teamTable = new Dictionary<Team, Dictionary<int, GameObject>>();
        teamTable[Team.Ally] = new Dictionary<int, GameObject>();
        teamTable[Team.Enemy] = new Dictionary<int, GameObject>();

        // Setup the player character
        CharacterData playerData = playerCharacter.GetComponentInChildren<CharacterData>();
        
        SetupCharacter(playerData.gameObject, Team.Ally);
        playerData.stats = InventoryController.Instance.statSheet;
        playerData.stats.InitializeStats();
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

    public void SpawnCharacters()
    {
        if (GameManager.Instance != null)
        {
            stageMission = Instantiate(GameManager.Instance.missionData); // Use GameManager's missionData instead of default if available - make a copy to not modify the original
        }
        else
        {
            stageMission = Instantiate(stageMission);
        }

        SpawnCharacter();
    }

    public void SpawnCharacter()
    {
        if (stageMission.enemyObjects.Count <= 0) return; // All enemies on stage spawned

        int enemyIndex = Random.Range(0, stageMission.enemyObjects.Count);
        GameObject enemyTemplate = stageMission.enemyObjects[enemyIndex];

        GameObject spawnedEnemy = spawnPoints[Random.Range(0, spawnPoints.Count)].SpawnObject(enemyTemplate);

        if (spawnedEnemy)
        {
            stageMission.numOpponents[enemyIndex]--;

            SetupCharacter(spawnedEnemy.GetComponentInChildren<CharacterData>().gameObject, Team.Enemy);

            // Stop spawning specific enemy if all of them are spawned
            if (stageMission.numOpponents[enemyIndex] <= 0)
                stageMission.enemyObjects.RemoveAt(enemyIndex);
        }

        if (IsInvoking(nameof(SpawnCharacter))) return;

        // Try again after waiting for a short time
        Invoke(nameof(SpawnCharacter), Random.Range(0.1f, 0.5f));
    }

    public void SetupCharacter(GameObject character, Team team)
    {
        // Set the characters
        CharacterData characterData;

        characterData = character.GetComponent<CharacterData>();

        characterData.id = characterCount;
        characterData.team = team;

        teamTable[team][characterData.id] = character;

        characterCount++;
    }

    private void Update()
    {
        if (IsInvoking(nameof(ReturnToLobby))) return;

        if (stageMission.CheckClearMission(teamTable[Team.Enemy].Count))
        {
            InventoryController.Instance.SetMoneyAmount(InventoryController.Instance.money + stageMission.reward);
            Invoke(nameof(ReturnToLobby), 3);
        }
        else if (stageMission.CheckFailMission(playerCharacter.GetComponentInChildren<CharacterData>().stats.currentHealth))
        {
            Invoke(nameof(ReturnToLobby), 3);
        }
    }

    public void SpawnAlly(GameObject character)
    {
        GameObject spawnedAlly = spawnPoints[Random.Range(0, spawnPoints.Count)].SpawnObject(character);

        if (spawnedAlly)
        {
            SetupCharacter(spawnedAlly.GetComponentInChildren<CharacterData>().gameObject, Team.Ally);
        }
        else
        {
            if (IsInvoking(nameof(SpawnAlly))) return;

            // Try again after waiting for a short time
            Invoke(nameof(SpawnAlly), Random.Range(0.1f, 0.5f));
        }
    }

    private void ReturnToLobby()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.SaveGame();

        SceneController.Instance.LoadScene("Lobby");
    }
}
