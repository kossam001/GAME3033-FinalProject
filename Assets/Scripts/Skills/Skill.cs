using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public AnimationClip animation;
    [Tooltip("Name of animation in animator to override")]
    public string overrideName;

    [Tooltip("The duration of an attack. Used for adjusting animation length to allow animation cancelling.")]
    public float attackDuration = 0.75f;
    [Tooltip("The validity period where one attack can be chained to another.")]
    public float comboDuration = 1.0f;

    [Tooltip("For if the skill is part of a chain")]
    public Skill followUpSkill;
    [Tooltip("ID for the combo")]
    public string comboName;

    public virtual void OverrideAnimationData(Animator animator, AnimatorOverrideController animatorOverrideController) { }
}
