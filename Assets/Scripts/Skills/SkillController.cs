using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Animator animator;
    public AnimatorOverrideController overrideController;

    private Skill activeSkill;
    public List<Socket> skillSockets; // If skill needs a collider, where is it
    private Dictionary<string, Socket> socketTable;

    public bool isActive = false; // A bool to start skill - indicates whether or not controller is doing something 
    public bool canCancel = false; // A bool to stop - the controller is doing something but can be stopped

    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private readonly int AttackSpeedHash = Animator.StringToHash("AttackSpeed");

    private void Start()
    {
        animator.runtimeAnimatorController = overrideController;
    }

    private void Awake()
    {
        socketTable = new Dictionary<string, Socket>();

        foreach (Socket socket in skillSockets)
        {
            socketTable.Add(socket.name, socket);
        }
    }

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
        StopAllCoroutines();

        if (CanChain(skill))
            activeSkill = activeSkill.followUpSkill; // If the used skill is from the same combo chain, use next chain
        else
            activeSkill = skill;

        isActive = true;
        canCancel = false;

        activeSkill.OverrideAnimationData(animator, overrideController);
        animator.SetBool(IsAttackingHash, true);

        animator.Play("SkillUse", 1, 0.0f);
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        float stateDuration = 0.0f;
        float stateLength = activeSkill.animation.length;
        float attackSpeed = animator.GetFloat(AttackSpeedHash);

        while (activeSkill.comboDuration * stateLength >= stateDuration)
        {
            // Turn attack collider on
            if (stateLength / attackSpeed * activeSkill.windupPeriod < stateDuration)
                ToggleCollider(activeSkill.socketName, true);

            stateDuration += Time.deltaTime;

            // If animation passes the noncancellable portion - the swing animation
            if (stateLength / attackSpeed * activeSkill.attackDuration < stateDuration)
                ToggleCollider(activeSkill.socketName, false); // Turn off collider

            if (stateLength / attackSpeed * activeSkill.noncancellablePeriod < stateDuration)
                canCancel = true;

            yield return null;
        }

        EndSkill();
    }

    private void ToggleCollider(string socketName, bool toggle)
    {
        Socket socket = socketTable[socketName];
        GameObject collider = socket.colliderObject;
        collider.SetActive(toggle);
    }

    // Returns whether or not skill was cancelled
    public bool CancelSkill()
    {
        if (canCancel)
        {
            ToggleCollider(activeSkill.socketName, false);
            animator.SetBool(IsAttackingHash, false);
        }

        return false;
    }

    public void EndSkill()
    {
        ToggleCollider(activeSkill.socketName, false);
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

    public void Interrupt()
    {
        if (isActive)
        {
            StopAllCoroutines();
            EndSkill();
        }
    }

    public float GetLength()
    {
        return activeSkill.animation.length / animator.GetFloat(AttackSpeedHash) * activeSkill.noncancellablePeriod;
    }
}
