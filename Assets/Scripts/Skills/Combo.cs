using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : StateMachineBehaviour
{
    private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private static readonly int ComboHash = Animator.StringToHash("Combo");
    private static readonly int ComboEndHash = Animator.StringToHash("ComboEnd");
    private readonly int CanCancelHash = Animator.StringToHash("CanCancel");

    private float stateDuration;
    [SerializeField] private float attackDuration = 0.75f;
    [SerializeField] private float comboDuration = 1.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stateDuration = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stateDuration += Time.deltaTime;

        // End animation early to chain attacks
        // The first check is to sync the IsAttackingHash
        if (stateDuration < stateInfo.length * attackDuration)
        {
            animator.SetBool(IsAttackingHash, true);
            animator.SetBool(CanCancelHash, false);
        }
        else if (stateDuration >= stateInfo.length * attackDuration)
        {
            animator.SetBool(CanCancelHash, true);
        }
        
        // End combo
        if (stateDuration >= stateInfo.length * comboDuration)
        {
            animator.SetInteger(ComboHash, -1);
            animator.SetBool(ComboEndHash, true);
            animator.SetBool(IsAttackingHash, false);
            animator.SetBool(CanCancelHash, false);
        }
    }

    //public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    //{
    //    if (animator.GetInteger(ComboHash) > 2)
    //        animator.SetInteger(ComboHash, -1);
    //}
}
