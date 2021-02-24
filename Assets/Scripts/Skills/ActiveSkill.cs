using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    public float range;
    public float angle;

    public float attackDuration; // How long the attack lasts
    public float castingTime; // Time it takes to use attack
    public float cooldown;
    public float cooldownTimer;

    public bool isCancellable; // Can another skill interrupt this skill
    public bool isStationary; // Can the character move while this skill is being used
    public bool canCancel; // Can this skill cancel another skill
    public bool isInterruptable; // Can the skill be interrupted by external influences (i.e. taking damage)
    public bool isInUse;

    public MeshRenderer meshRenderer;
    public string target; // Using tags

    public virtual IEnumerator Use() { yield return null; }
}
