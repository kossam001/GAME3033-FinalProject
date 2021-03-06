using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public GameObject character;
    public AIController controller;

    public Vector3 moveDestination;

    public List<GameObject> enemyTargets;
    public GameObject activeTarget;

    [Tooltip("Maximum distance to a target to be considered in range of combat.")]
    public float combatRange;

    public SkillList skillList;
    public Skill selectedSkill;

    public NavMeshAgent agent;
    
    public float GetDistanceFromTarget()
    {
        return Vector3.Distance(character.transform.position, activeTarget.transform.position);
    }

    public Vector3 GetDirectionToTarget()
    {
        return activeTarget.transform.position - character.transform.position;
    }
}
