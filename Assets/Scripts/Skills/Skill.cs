using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    [Header("Parameters")]
    public string skillName;
    public float cooldown = 0.0f;
    public float cost = 0.0f;

    [Header("Collider Physics")]
    [Tooltip("Name of associated socket")]
    public string socketName;

    [Header("Animation")]
    public AnimationClip animation;
    [Tooltip("Name of animation in animator to override")]
    public string overrideName;

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
}
