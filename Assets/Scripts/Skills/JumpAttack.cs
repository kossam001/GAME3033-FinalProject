using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "Skills/JumpAttack")]
public class JumpAttack : BasicAttack3
{
    private Vector3 originalScale;

    public override void PrestartEffect(SkillController skillController)
    {
        base.PrestartEffect(skillController);

        originalScale = colliderObject.transform.localScale;
        colliderObject.transform.localScale = scale;
    }

    public override void EndEffect(SkillController skillController)
    {
        base.PrestartEffect(skillController);

        base.EndEffect(skillController);
        colliderObject.transform.localScale = originalScale;
    }
}
