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

    public override void PrestartEffect(SkillController skillController)
    {
        if (prestartEffectActivated) return;
        base.PrestartEffect(skillController);

        Socket socket = skillController.RetrieveSocket(socketName);
        colliderObject = socket.colliderObject;
        DamageCollider collider = colliderObject.GetComponent<DamageCollider>();

        collider.action = ApplyEffect;
    }

    public override void StartEfftect(SkillController skillController)
    {
        if (isRepeating()) return;
        base.StartEfftect(skillController);

        Socket socket = skillController.RetrieveSocket(socketName);
        colliderObject = socket.colliderObject;

        colliderObject.SetActive(true);
    }

    public override void EndEffect(SkillController skillController)
    {
        if (!startEffectActivated) return;
        base.EndEffect(skillController);

        Socket socket = skillController.RetrieveSocket(socketName);
        GameObject collider = socket.colliderObject;
        collider.SetActive(false);
    }

    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        switch (caster.tag)
        {
            case "Ally":
                if (target.CompareTag("Enemy"))
                {
                    target.GetComponent<CharacterData>().UpdateHealth(damage);
                    target.GetComponent<CharacterData>().knockbackComponent.Flinch(knockback);
                }
                break;
            case "Enemy":
                if (target.CompareTag("Ally"))
                {
                    target.GetComponent<CharacterData>().UpdateHealth(damage);
                    target.GetComponent<CharacterData>().knockbackComponent.Flinch(knockback);
                }
                break;
        }
    }
}
