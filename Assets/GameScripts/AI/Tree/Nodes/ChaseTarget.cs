using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AITreeNodes/Chase")]
public class ChaseTarget : TreeNode
{
    [SerializeField] protected float stoppingRange;

    public override bool PerformCheck()
    {
        if (brain.activeTarget == null)
        {
            brain.agent.ResetPath();
            state.ChangeState(StateID.InCombat);
            return false;
        }

        Vector3 targetPosition = brain.activeTarget.transform.position;
        Vector3 selfPosition = brain.character.transform.position;

        float distance = Vector3.Distance(targetPosition, selfPosition);

        if (distance > stoppingRange)
        {
            brain.moveDestination = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));
        }
        else
        {
            brain.agent.ResetPath();
            state.ChangeState(StateID.InCombat);

            return false;
        }

        return true;
    }

    public override bool Run()
    {
        if (!PerformCheck()) return false;

        return base.Run();
    }
}
