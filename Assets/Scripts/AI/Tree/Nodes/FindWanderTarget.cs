using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindWanderTarget", menuName = "AITreeNodes/FindWanderTarget")]
public class FindWanderTarget : TreeNode
{
    public float wanderRange = 10.0f;
    public float moveDistance = 5.0f;

    public override bool Run()
    {
        if (!brain.agent.hasPath || brain.agent.remainingDistance <= 0.1f)
            brain.moveDestination = new Vector3
                (
                    Random.Range(-wanderRange + brain.activeTarget.transform.position.x, wanderRange + brain.activeTarget.transform.position.x), 
                    0.0f, 
                    Random.Range(-wanderRange + brain.activeTarget.transform.position.z, wanderRange + brain.activeTarget.transform.position.z)
                );

        else if (wanderRange <= Vector3.Distance(brain.character.transform.position, brain.activeTarget.transform.position))
        {
            state.ChangeState(StateID.Chase);
            return false;
        }

        return base.Run();
    }
}
