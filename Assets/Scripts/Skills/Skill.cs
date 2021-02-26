using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public AnimationClip animation;

    [Tooltip("For if the skill is part of a chain")]
    public bool isCombo;

    public virtual void OverrideAnimationData(string animationName, Animator animator, AnimatorOverrideController animatorOverrideController) { }
}
