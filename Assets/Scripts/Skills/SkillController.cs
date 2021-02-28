using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Animator animator;
    public AnimatorOverrideController overrideController;

    private Skill activeSkill;

    public bool isActive = false; // A bool to start skill - indicates whether or not controller is doing something 
    public bool canCancel = false; // A bool to stop - the controller is doing something but can be stopped

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public void Use(Skill skill, string overrideName)
    {
        // Another skill is being used
        if (!canCancel && isActive) return;

        ActivateSkill(skill);
    }

    private bool CanChain(Skill skill)
    {
        return isActive && activeSkill.comboName == skill.comboName && canCancel && activeSkill.followUpSkill != null;
    }

    private void ActivateSkill(Skill skill)
    {
        if (CanChain(skill))
            activeSkill = activeSkill.followUpSkill; // If the used skill is from the same combo chain, use next chain
        else
            activeSkill = skill; 

        StopAllCoroutines();

        isActive = true;
        canCancel = false;

        activeSkill.OverrideAnimationData(animator, overrideController);
        animator.SetBool(IsAttackingHash, true);

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
                canCancel = true;

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
        if (canCancel)
            animator.SetBool(IsAttackingHash, false);

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
