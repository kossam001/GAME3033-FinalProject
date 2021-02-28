using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindActiveTarget", menuName = "AITreeNodes/FindActiveTarget")]
public class FindActiveTarget : TreeNode
{
    [Tooltip("Debug")]
    public GameObject activeTarget;

    public override bool Run()
    {
        if (brain.activeTarget == null)
        {
            brain.activeTarget = brain.enemyTargets[Random.Range(0, brain.enemyTargets.Count-1)];
            activeTarget = brain.activeTarget;
        }

        return base.Run();
    }
}
