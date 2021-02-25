using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : StateMachineBehaviour
{
    private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private static readonly int ComboHash = Animator.StringToHash("Combo");
    private static readonly int ComboEndHash = Animator.StringToHash("ComboEnd");

    private float stateDuration;
    [SerializeField] private float attackDuration = 0.75f;

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
        }
        else if (stateDuration >= stateInfo.length * attackDuration)
        {
            animator.SetBool(IsAttackingHash, false);
        }
        
        // End combo
        if (stateDuration >= stateInfo.length)
        {
            animator.SetInteger(ComboHash, -1);
            animator.SetBool(ComboEndHash, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
