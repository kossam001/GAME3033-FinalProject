using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public AnimationClip animation;

    [Tooltip("For if the skill is part of a chain")]
    public Skill nextChain;

    public virtual void OverrideAnimationData(string animationName, Animator animator, AnimatorOverrideController animatorOverrideController) { }
}
