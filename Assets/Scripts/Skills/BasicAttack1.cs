using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack1", menuName = "Skills/BasicAttack1")]
public class BasicAttack1 : Skill
{
    public override void OverrideAnimationData(Animator animator, AnimatorOverrideController animatorOverrideController)
    {
        animatorOverrideController[overrideName] = animation;
    }

    public override void StartEfftect(SkillController skillController)
    {
        base.StartEfftect(skillController);

        if (isRepeating()) return;

        Socket socket = skillController.RetrieveSocket(socketName);
        GameObject collider = socket.colliderObject;
        collider.SetActive(true);
    }

    public override void EndEffect(SkillController skillController)
    {
        if (!effectActivated) return;
        base.EndEffect(skillController);

        Socket socket = skillController.RetrieveSocket(socketName);
        GameObject collider = socket.colliderObject;
        collider.SetActive(false);
    }
}
