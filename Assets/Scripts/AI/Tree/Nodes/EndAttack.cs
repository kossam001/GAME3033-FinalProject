using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndAttack", menuName = "AITreeNodes/EndAttack")]
public class EndAttack : TreeNode
{
    public override bool Run()
    {
        brain.controller.CancelSkill();
        brain.agent.isStopped = false;
        state.ChangeState(StateID.InCombat);

        return base.Run();
    }
}
