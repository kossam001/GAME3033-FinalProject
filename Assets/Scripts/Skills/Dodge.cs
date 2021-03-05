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

    public override void PrestartEffect(CharacterData character)
    {
        if (prestartEffectActivated) return;
        base.PrestartEffect(character);

        character.characterAnimator.SetFloat(DodgeSpeedHash, speed);
    }

    public override void StartEfftect(CharacterData character)
    {
        if (isRepeating()) return;
        base.StartEfftect(character);

        ownerTag = character.tag;

        if (!character.canMove) return;

        character.tag = "Untagged";
        character.canMove = false;
        character.characterAnimator.SetBool(IsDodgingHash, true);
    }

    public override void EndEffect(CharacterData character)
    {
        if (!startEffectActivated) return;
        base.EndEffect(character);

        character.characterAnimator.SetBool(IsDodgingHash, false);
        character.tag = ownerTag;
        character.canMove = true;
    }
}
