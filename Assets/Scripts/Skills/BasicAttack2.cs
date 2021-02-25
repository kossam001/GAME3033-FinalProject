using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack2", menuName = "Skills/BasicAttack2")]
public class BasicAttack2 : Skill
{
    [Tooltip("Length of the animation that is dedicated to the actual attack")]
    [SerializeField] private float attackDuration = 0.6f;

    [Tooltip("When to end the attack, can end earlier if necessary")]
    [SerializeField] private float comboDuration = 1.0f;

    public override void OverrideAnimationData(string animationName, Animator animator, AnimatorOverrideController animatorOverrideController)
    {
        animatorOverrideController[animationName] = animation;
        animator.GetBehaviour<Combo>().SetComboParameters(attackDuration, comboDuration);
    }
}
