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

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        
        else
            instance = this;

        foreach (GameObject character in enemies)
        {
            character.GetComponent<CharacterData>().team = Team.Enemy;
        }

        foreach (GameObject character in allies)
        {
            character.GetComponent<CharacterData>().team = Team.Ally;
        }
    }

    public List<GameObject> GetEnemies(Team team)
    {
        if (team == Team.Ally)
            return enemies;
        else
            return allies;
    }

    public List<GameObject> GetFriends(Team team)
    {
        if (team == Team.Ally)
            return allies;
        else
            return enemies;
    }
}
