using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Mission", order = 1)]
public class Mission : Item
{
    [Tooltip("Each index is a number for a different type of enemy.")]
    public List<int> numOpponents; 
    [Tooltip("Enemy Objects - parallel to numOpponents array.")]
    public List<GameObject> enemyObjects;

    public int reward;
    public string missionInfo;
    public string stageName;

    public bool CheckClearMission(int enemyCount)
    {
        if (enemyCount == 0) return true;

        return false;
    }

    public bool CheckFailMission(int playerHealth)
    {
        if (playerHealth <= 0) 
            return true;

        return false;
    }
}
