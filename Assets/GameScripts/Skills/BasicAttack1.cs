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

    public override void PrestartEffect(CharacterData character)
    {
        if (prestartEffectActivated) return;
        base.PrestartEffect(character);

        Socket socket = character.RetrieveSocket(socketName);
        colliderObject = socket.colliderObject;
        DamageCollider collider = colliderObject.GetComponent<DamageCollider>();

        collider.action = ApplyEffect;
    }

    public override void StartEfftect(CharacterData character)
    {
        if (isRepeating()) return;
        base.StartEfftect(character);

        Socket socket = character.RetrieveSocket(socketName);
        colliderObject = socket.colliderObject;

        colliderObject.SetActive(true);
    }

    public override void EndEffect(CharacterData character)
    {
        if (!startEffectActivated) return;
        base.EndEffect(character);

        Socket socket = character.RetrieveSocket(socketName);
        GameObject collider = socket.colliderObject;
        collider.SetActive(false);
    }

    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        if (!target.GetComponent<CharacterData>()) return;

        CharacterData targetData = target.GetComponent<CharacterData>();
        CharacterData casterData = caster.GetComponent<CharacterData>();

        switch (caster.tag)
        {
            case "Ally":
                if (target.CompareTag("Enemy"))
                {
                    targetData.UpdateHealth(targetData.stats.DamageCalculation(casterData.stats.attack + damage));
                    target.GetComponent<CharacterData>().knockbackComponent.Flinch(knockback);
                }
                break;
            case "Enemy":
                if (target.CompareTag("Ally"))
                {
                    targetData.UpdateHealth(targetData.stats.DamageCalculation(casterData.stats.attack + damage));
                    target.GetComponent<CharacterData>().knockbackComponent.Flinch(knockback);
                }
                break;
        }
    }
}
