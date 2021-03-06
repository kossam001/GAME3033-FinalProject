﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    None,
    BasicAttack,
    AltAttack,
    HeavyAttack,
    AltHeavyAttack,
    Evade
}

public class Skill : ScriptableObject
{
    [Header("Parameters")]
    public SkillID skillID;
    public float cooldown = 0.0f;
    public float cost = 0.0f;
    public float speed = 1.0f;
    public float knockback = 0.0f;
    public int damage = 10;
    [Tooltip("Attack effect should be repeated during the duration of the attack.")]
    public bool repeatEffect = false;
    [Tooltip("Can the skill interrupt other skills.")]
    public bool canInterrupt = false;
    public bool canBeInterrupted = false;
    [Tooltip("Can flinch interrupt the attack.")]
    public bool canResistFlinch = false;

    protected bool prestartEffectActivated = false;
    protected bool startEffectActivated = false;

    [Header("Collider Physics")]
    [Tooltip("Name of associated socket.  Used to retrieve collider.  " +
        "It is to be able to access other sockets other than the socket the weapon is attached to for skills that uses different parts of the character. E.x. kicks, punches, dual wielding")]
    public string socketName;
    protected GameObject colliderObject;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    [Header("Animation")]
    public AnimationClip animation;
    [Tooltip("Name of animation in animator to override")]
    public string overrideName;
    [Tooltip("Whether or not the animation should be used externally.")]
    public bool useAnimation = true;
    [Tooltip("Whether there is motion in the animation.")]
    public float movementForce1;
    public float movementForce2;
    public float movementForce3;

    [Header("Timing")]
    [Tooltip("Period before the attack - collider should be off.")]
    public float windupPeriod = 0.1f;
    [Tooltip("The duration of an attack. For toggling collider.")]
    public float attackDuration = 0.75f;
    [Tooltip("The validity period where one attack can be chained to another.")]
    public float comboDuration = 1.0f;
    [Tooltip("Used for adjusting animation length to allow animation cancelling.")]
    public float noncancellablePeriod = 0.75f;

    [Header("Combo")]
    [Tooltip("For if the skill is part of a chain")]
    public Skill followUpSkill;
    [Tooltip("ID for the combo")]
    public string comboName;

    [Header("AI")]
    [Tooltip("Min range for the skill to be usable.")]
    public float minRange = 0.0f;
    [Tooltip("Max range for the skill to be usable.")]
    public float maxRange = 1.0f;
    [Tooltip("Arc range of the attack.")]
    public float arcAngle;

    public virtual void OverrideAnimationData(Animator animator, AnimatorOverrideController animatorOverrideController) { }

    public virtual void PrestartEffect(CharacterData character)
    {
        prestartEffectActivated = true;
    }

    public virtual void StartEfftect(CharacterData character)
    {
        prestartEffectActivated = false;
        startEffectActivated = true;
    }

    public virtual void EndEffect(CharacterData character)
    {
        prestartEffectActivated = false;
        startEffectActivated = false;
    }

    public bool isRepeating()
    {
        if (!repeatEffect && startEffectActivated) return true;

        return false;
    }

    public virtual void ApplyEffect(GameObject target, GameObject caster) { }
}
