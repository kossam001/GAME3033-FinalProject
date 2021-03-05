using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public CharacterData owner;

    private Skill activeSkill;

    public bool isActive = false; // A bool to start skill - indicates whether or not controller is doing something 
    public bool canCancel = false; // A bool to stop - the controller is doing something but can be stopped
    public bool isStopped = false; // A bool to stop - the controller is doing something but can be stopped

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private readonly int AttackSpeedHash = Animator.StringToHash("AttackSpeed");

    private IEnumerator playAnimationRoutine;

    public void Use(Skill skill, string overrideName)
    {
        // Another skill is being used
        if (!canCancel && isActive && (!skill.canInterrupt || !activeSkill.canBeInterrupted)) return;

        if (skill.canInterrupt && activeSkill != null && activeSkill.canBeInterrupted)
            Interrupt();


        if (isStopped) return;

        ActivateSkill(skill);
    }

    private bool CanChain(Skill skill)
    {
        return isActive && activeSkill.comboName == skill.comboName && canCancel && activeSkill.followUpSkill != null;
    }

    private void ActivateSkill(Skill skill)
    {
        if (playAnimationRoutine != null)
            StopCoroutine(playAnimationRoutine);

        if (CanChain(skill))
            activeSkill = activeSkill.followUpSkill; // If the used skill is from the same combo chain, use next chain
        else
            activeSkill = skill;

        isActive = true;
        canCancel = false;

        if (activeSkill.useAnimation)
        {
            activeSkill.OverrideAnimationData(owner.characterAnimator, owner.animatorOverride);
            owner.characterAnimator.SetBool(IsAttackingHash, true);

            owner.characterAnimator.Play("SkillUse", 1, 0.0f);
        }

        playAnimationRoutine = PlayAnimation();
        StartCoroutine(playAnimationRoutine);
    }

    public IEnumerator PlayAnimation()
    {
        float stateDuration = 0.0f;
        float stateLength = activeSkill.animation.length;
        float attackSpeed = activeSkill.speed;
        owner.characterAnimator.SetFloat(AttackSpeedHash, attackSpeed);

        while (activeSkill.comboDuration / attackSpeed * stateLength >= stateDuration)
        {
            stateDuration += Time.deltaTime;

            if (stateLength / attackSpeed * activeSkill.windupPeriod > stateDuration)
                activeSkill.PrestartEffect(owner);

            // Turn attack collider on
            if (stateLength / attackSpeed * activeSkill.windupPeriod < stateDuration &&
                stateLength / attackSpeed * activeSkill.attackDuration > stateDuration)
                activeSkill.StartEfftect(owner); // Trigger skill effect

            // If animation passes the noncancellable portion - the swing animation
            if (stateLength / attackSpeed * activeSkill.attackDuration < stateDuration &&
                activeSkill.comboDuration * stateLength > stateDuration)
                activeSkill.EndEffect(owner); // Turn off skill effect

            if (stateLength / attackSpeed * activeSkill.noncancellablePeriod < stateDuration)
                canCancel = true;

            yield return null;
        }

        EndSkill();
    }

    // Returns whether or not skill was cancelled
    public bool CancelSkill()
    {
        if (canCancel)
        {
            //ToggleCollider(activeSkill.socketName, false);
            activeSkill.EndEffect(owner);
            owner.characterAnimator.SetBool(IsAttackingHash, false);
        }

        return false;
    }

    public void EndSkill()
    {
        activeSkill.EndEffect(owner);
        isActive = false;
        canCancel = false;
        activeSkill = null;
        owner.characterAnimator.SetBool(IsAttackingHash, false);
    }

    public bool SkillInUse()
    {
        if (canCancel == false && activeSkill == false)
            return false;

        return true;
    }

    public void Interrupt()
    {
        if (isActive)
        {
            if (playAnimationRoutine != null)
                StopCoroutine(playAnimationRoutine);
            EndSkill();
        }
    }

    public void Stop(float duration)
    {
        if (isStopped) return;

        isStopped = true;
        StartCoroutine(Stopping(duration));
    }

    public IEnumerator Stopping(float duration)
    {
        while (duration >= 0.0f)
        {
            duration -= Time.deltaTime;

            yield return null;
        }

        isStopped = false;
    }

    public float GetLength()
    {
        return activeSkill.animation.length / owner.characterAnimator.GetFloat(AttackSpeedHash) * activeSkill.noncancellablePeriod;
    }

    public string GetActiveSkillName()
    {
        if (activeSkill != null)
            return activeSkill.skillName;

        else
            return "Invalid41245161";
    }

    public bool CanInterrupt()
    {
        if (activeSkill == null) return true;

        return activeSkill.canInterrupt;
    }

    public bool CanResistFlinch()
    {
        if (activeSkill == null) return false;

        return activeSkill.canResistFlinch;
    }

    public Animator GetAnimator()
    {
        return owner.characterAnimator;
    }
}
