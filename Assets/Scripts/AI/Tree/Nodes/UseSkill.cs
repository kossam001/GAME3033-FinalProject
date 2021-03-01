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

        // Counts the period where the character is stuck in an attack animation
        if (brain.skillController.isActive)
            timer -= Time.deltaTime;

        if (brain.selectedSkill.maxRange >= distance &&
            brain.selectedSkill.minRange <= distance &&
            (!brain.skillController.isActive ||
            brain.skillController.canCancel))
        {
            brain.skillController.Use(brain.selectedSkill, brain.selectedSkill.overrideName);
            // Get the period where the character can't move
            timer = brain.skillController.GetCurrentNoncancellableSkillLength();

            // Stop agent
            brain.agent.isStopped = true;
            return true;
        }

        if (timer <= 0.0f && brain.skillController.canCancel)
        {
            brain.skillController.CancelSkill();
            brain.agent.isStopped = false;
        }

        return false;
    }
}
