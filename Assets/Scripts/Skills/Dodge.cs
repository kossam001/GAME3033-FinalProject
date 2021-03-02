using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "Skills/Dodge")]
public class Dodge : Skill
{
    // Animator hashes
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");
    private readonly int IsDodgingHash = Animator.StringToHash("IsDodging");
    private readonly int DodgeSpeedHash = Animator.StringToHash("DodgeSpeed");

    private string ownerTag;

    public override void StartEfftect(SkillController skillController)
    {
        if (isRepeating()) return;
        base.StartEfftect(skillController);

        ownerTag = skillController.owner.tag;

        if (!skillController.owner.canMove) return;

        skillController.owner.tag = "Untagged";
        skillController.owner.canMove = false;
        skillController.animator.SetBool(IsDodgingHash, true);
    }

    public override void EndEffect(SkillController skillController)
    {
        if (!startEffectActivated) return;
        base.EndEffect(skillController);

        skillController.animator.SetFloat(MoveXHash, 0.0f);
        skillController.animator.SetFloat(MoveZHash, 0.0f);

        skillController.animator.SetBool(IsDodgingHash, false);
        skillController.owner.tag = ownerTag;
        skillController.owner.canMove = true;
    }
}
