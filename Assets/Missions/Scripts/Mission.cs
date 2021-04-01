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
}
