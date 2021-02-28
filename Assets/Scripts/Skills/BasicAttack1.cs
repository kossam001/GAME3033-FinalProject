using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack1", menuName = "Skills/BasicAttack1")]
public class BasicAttack1 : Skill
{
    public override void OverrideAnimationData(Animator animator, AnimatorOverrideController animatorOverrideController)
    {
        animatorOverrideController[overrideName] = animation;
        //animator.GetBehaviour<Combo>().SetComboParameters(attackDuration, comboDuration);
    }
}
