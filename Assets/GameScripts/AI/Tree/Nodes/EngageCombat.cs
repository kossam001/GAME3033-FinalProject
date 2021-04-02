using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EngageCombat", menuName = "AITreeNodes/EngageCombat")]
public class EngageCombat : TreeNode
{
    public override bool Run()
    {
        if (ProcCheck())
        {
            state.ChangeState(StateID.Attack);
        }

        return base.Run();
    }
}
