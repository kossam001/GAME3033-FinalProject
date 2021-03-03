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

        // First check - to cancel a combo randomly
        if (timer <= 0.0f
            && (ProcCheck() && brain.controller.skillController.isActive && brain.controller.skillController.canCancel)) 
        {
            return base.Run();
        }

        // Counts the period where the character is stuck in an attack animation
        if (brain.controller.skillController.isActive)
            timer -= Time.deltaTime;

        float angle = Vector3.Angle(brain.character.transform.forward, brain.GetDirectionToTarget());

        if (brain.selectedSkill.maxRange >= distance &&
            brain.selectedSkill.minRange <= distance &&
            brain.selectedSkill.arcAngle >= Vector3.Angle(brain.character.transform.forward, brain.GetDirectionToTarget()) &&
            (!brain.controller.skillController.isActive ||
            brain.controller.skillController.canCancel))
        {
            if (brain.controller.skillController.isStopped) return false;

            brain.controller.UseSkill(brain.selectedSkill);
            // Get the period where the character can't move
            timer = brain.controller.skillController.GetLength();

            return true;
        }

        // Check two - cancel cannot be completed
        if (timer <= 0.0f && brain.controller.skillController.canCancel)
            return base.Run();

        return false;
    }
}
