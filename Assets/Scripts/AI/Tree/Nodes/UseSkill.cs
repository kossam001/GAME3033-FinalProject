using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseSkill", menuName = "AITreeNodes/UseSkill")]
public class UseSkill : TreeNode
{
    public override bool Run()
    {
        if (brain.selectedSkill.maxRange <= brain.GetDistanceFromTarget() &&
            brain.selectedSkill.minRange >= brain.GetDistanceFromTarget())
        {
            brain.skillController.Use(brain.selectedSkill, brain.selectedSkill.overrideName);
        }

        return base.Run();
    }
}
