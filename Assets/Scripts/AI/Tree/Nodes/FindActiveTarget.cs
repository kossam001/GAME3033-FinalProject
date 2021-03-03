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
        if (brain.activeTarget == null || ProcCheck())
        {
            List<GameObject> targets = StageManager.Instance.GetEnemies(brain.character.GetComponent<CharacterData>().team);
            if (targets.Count <= 0) return false;

            brain.activeTarget = targets[Random.Range(0, targets.Count)];
            activeTarget = brain.activeTarget;
        }

        return base.Run();
    }
}
