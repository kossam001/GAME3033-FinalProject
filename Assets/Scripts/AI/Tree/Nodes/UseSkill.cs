using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseSkill", menuName = "AITreeNodes/UseSkill")]
public class UseSkill : TreeNode
{
    private float timer = 0.0f;

    public override bool Run()
    {
        float distance = brain.GetDistanceFromTarget();

        timer -= Time.deltaTime;

        if (brain.selectedSkill.maxRange >= distance &&
            brain.selectedSkill.minRange <= distance &&
            timer <= 0.0f)
        {
            brain.skillController.Use(brain.selectedSkill, brain.selectedSkill.overrideName);
            timer = brain.selectedSkill.noncancellablePeriod;
        }

        return base.Run();
    }
}
