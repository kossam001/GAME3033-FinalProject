using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skills that needs to be activated.
// Examples: attacks, support magic
public class ActiveSkill : Skill
{
    [Header("AI parameters.")]
    [Tooltip("How far the attack can reach.")]
    public float range;
    [Tooltip("How wide the attack is.")]
    public float angle;
    [Tooltip("Intended target. Can be player or ally depending on behaviour.")]
    public string target; // Using tags

    [Header("Effect/Animation parameters")]
    [Tooltip("How long the attack lasts. The portion where attack collider is active.")]
    public float attackDuration; 
    [Tooltip("For attacks that can be charged.")]
    public float chargingTime;

    [Header("Cooldown")]
    [Tooltip("Time between each usage.")]
    public float cooldown;

    protected float cooldownTimer; // Keep track of the cooldown timer

    [Header("Details")]
    [Tooltip("Can another skill interrupt this skill")]
    public bool isCancellable; // Can another skill interrupt this skill
    [Tooltip("Can the character move while this skill is being used")]
    public bool isStationary; // Can the character move while this skill is being used
    [Tooltip("Can this skill cancel another skill")]
    public bool canCancel; // Can this skill cancel another skill
    [Tooltip("Can the skill be interrupted by external influences (i.e. taking damage)")]
    public bool isInterruptable; // Can the skill be interrupted by external influences (i.e. taking damage)
    [Tooltip("Is a skill currently in use. Prevents certain actions from being taken.")]
    public bool isInUse;

    public int damage;
    [Tooltip("Can have hitbox, but can also have auto lockon.")]
    public Collider hitBox;

    public MeshRenderer meshRenderer;

    public virtual IEnumerator Use() { yield return null; }
}
