using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "AITreeNodes/MoveToTarget")]
public class MoveToTarget : TreeNode
{
    public override bool Run()
    {
        if (brain.agent.destination != brain.moveDestination)
        {
            brain.agent.SetDestination(brain.moveDestination);
        }

        return base.Run();
    }
}
