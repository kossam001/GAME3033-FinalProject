using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SkillList : MonoBehaviour
{
    [SerializeField] private List<Skill> skills;

    public Dictionary<SkillID, Skill> skillTable;

    private void Awake()
    {
        skillTable = new Dictionary<SkillID, Skill>();

        for (int i = 0; i < skills.Count; i++)
        {
            skillTable.Add(skills[i].skillID, skills[i]);
        }
    }

    public Skill GetRandomSkill()
    {
        return skills[Random.Range(0, skills.Count)];
    }
}