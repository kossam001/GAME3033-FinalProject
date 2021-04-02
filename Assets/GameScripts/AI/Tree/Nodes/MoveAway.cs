using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAway", menuName = "AITreeNodes/MoveAway")]
public class MoveAway : TreeNode
{
    public float minDistance = 5.0f;
    public float maxMoveAwayDistance = 10.0f;

    public override bool Run()
    {
        if (brain.GetDistanceFromTarget() <= minDistance)
        {
            brain.moveDestination = -brain.GetDirectionToTarget().normalized * Random.Range(minDistance, maxMoveAwayDistance);
        }

        return base.Run();
    }
}
