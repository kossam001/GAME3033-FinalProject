using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "AITreeNodes/MoveToTarget")]
public class MoveToTarget : TreeNode
{
    public override bool Run()
    {
        brain.controller.SetCurrentNode(this);

        if (!brain.agent.hasPath || brain.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            brain.agent.SetDestination(brain.moveDestination);
        }

        return base.Run();
    }
}
