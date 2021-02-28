using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Animator animator;
    public AnimatorOverrideController overrideController;

    private Skill activeSkill;

    public bool isActive = false;
    public bool canCancel = false;

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public void Use(Skill skill, string overrideName)
    {
        // Another skill is being used
        if (!canCancel && isActive) return;

        // Allowing the skill controller to set an active skill, specifically for skills that follow a chain
        if (isActive && activeSkill.comboName == skill.comboName && canCancel)
        {
            ChainSkill(skill);
        }
        else
        {
            activeSkill = skill;

            isActive = true;
            canCancel = false;

            skill.OverrideAnimationData(animator, overrideController);
            animator.SetBool(IsAttackingHash, true);

            StartCoroutine(PlayAnimation());
        }
    }

    private void ChainSkill(Skill skill)
    {
        if (activeSkill.followUpSkill != null)
            activeSkill = activeSkill.followUpSkill; // If the used skill is from the same combo chain, use next chain
        else
            activeSkill = skill; // Cycle around

        activeSkill.OverrideAnimationData(animator, overrideController);
        animator.SetBool(IsAttackingHash, true);

        StopAllCoroutines();

        canCancel = false;

        animator.Play("SkillUse", 1, 0.0f);
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        // Animation does not immediately play when 
        yield return StartCoroutine(WaitForStateTransition("SkillUse"));

        float stateDuration = 0.0f;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(1);

        while (activeSkill.comboDuration * stateInfo.length >= stateDuration)
        {
            stateDuration += Time.deltaTime;

            // If animation passes the noncancellable portion
            if (stateInfo.length * activeSkill.attackDuration < stateDuration)
            {
                canCancel = true;
            }

            //// If the animation state is finished
            //if (stateInfo.length <= stateDuration)
            //{
            //    animator.SetBool(IsAttackingHash, false); // Go back to idle state
            //}

            yield return null;
        }

        EndSkill();
    }

    private IEnumerator WaitForStateTransition(string stateName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(1).IsName(stateName))
        {
            yield return null;
        }
    }

    // Returns whether or not skill was cancelled
    public bool CancelSkill()
    {
        // Setting up next skill in chain
        //if (activeSkill.followUpSkill != null)
        //{
        //    activeSkill = activeSkill.followUpSkill;
        //    stateDuration = 0.0f;

        //    return true;
        //}
        if (canCancel)
        {
            animator.SetBool(IsAttackingHash, false);
        }

        return false;
    }

    public void EndSkill()
    {
        isActive = false;
        canCancel = false;
        activeSkill = null;
        animator.SetBool(IsAttackingHash, false);
    }

    public bool SkillInUse()
    {
        if (canCancel == false && activeSkill == false)
            return false;

        return true;
    }
}
