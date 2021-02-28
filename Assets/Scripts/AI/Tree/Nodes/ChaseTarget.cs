using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AITreeNodes/Chase")]
public class ChaseTarget : TreeNode
{
    public override bool PerformCheck()
    {
        Vector3 targetPosition = brain.activeTarget.transform.position;
        Vector3 selfPosition = brain.character.transform.position;

        if (Vector3.Distance(targetPosition, selfPosition) > brain.combatRange)
        {
            brain.agent.isStopped = false;
            brain.moveDestination = targetPosition;
        }
        else
        {
            brain.agent.isStopped = true;
        }

        return true;
    }

    public override bool Run()
    {
        brain.controller.SetCurrentNode(this);

        if (!PerformCheck()) return false;

        return base.Run();
    }
}
