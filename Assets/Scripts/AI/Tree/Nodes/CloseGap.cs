using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CloseGap", menuName = "AITreeNodes/CloseGap")]
public class CloseGap : ChaseTarget
{
    public override bool Run()
    {
        if (brain.selectedSkill != null && 
            (stoppingRange < brain.selectedSkill.minRange ||
            stoppingRange > brain.selectedSkill.maxRange))
        {
            stoppingRange = Random.Range(brain.selectedSkill.minRange, brain.selectedSkill.maxRange);
        }

        return base.Run();
    }
}
