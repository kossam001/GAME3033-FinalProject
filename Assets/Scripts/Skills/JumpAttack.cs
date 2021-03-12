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

        character.GetComponent<Rigidbody>().AddForce(100 * character.transform.forward);
    }

    public override void StartEfftect(CharacterData character)
    {
        base.StartEfftect(character);
    }

    public override void EndEffect(CharacterData character)
    {
        base.PrestartEffect(character);

        base.EndEffect(character);
        colliderObject.transform.localScale = originalScale;
    }
}
