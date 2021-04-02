using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectSkill", menuName = "AITreeNodes/SelectSkill")]
public class SelectSkill : TreeNode
{
    public override bool Run()
    {
        if (brain.selectedSkill == null)
            brain.selectedSkill = brain.skillList.GetRandomSkill();

        return base.Run();
    }
}
