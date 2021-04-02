using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "Skills/JumpAttack")]
public class JumpAttack : BasicAttack3
{
    private Vector3 originalScale;

    public override void PrestartEffect(CharacterData character)
    {
        base.PrestartEffect(character);

        originalScale = colliderObject.transform.localScale;
        colliderObject.transform.localScale = scale;
    }

    public override void StartEfftect(CharacterData character)
    {
        base.StartEfftect(character);
    }

    public override void EndEffect(CharacterData character)
    {
        base.EndEffect(character);

        base.EndEffect(character);
        colliderObject.transform.localScale = originalScale;
    }
}
