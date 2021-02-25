using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack3", menuName = "Skills/BasicAttack3")]
public class BasicAttack3 : Skill
{
    [Tooltip("Length of the animation that is dedicated to the actual attack")]
    [SerializeField] private float attackDuration = 0.7f;

    [Tooltip("When to end the attack, can end earlier if necessary")]
    [SerializeField] private float comboDuration = 0.7f;

    public override void OverrideAnimationData(string animationName, Animator animator, AnimatorOverrideController animatorOverrideController)
    {
        animatorOverrideController[animationName] = animation;
        animator.GetBehaviour<Combo>().SetComboParameters(attackDuration, comboDuration);
    }
}
