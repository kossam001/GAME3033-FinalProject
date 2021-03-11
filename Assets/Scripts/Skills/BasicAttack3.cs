using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack3", menuName = "Skills/BasicAttack3")]
public class BasicAttack3 : BasicAttack1
{
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
                    target.GetComponent<CharacterData>().knockbackComponent.Knockdown(knockback);
                }
                break;
            case "Enemy":
                if (target.CompareTag("Ally"))
                {
                    targetData.UpdateHealth(targetData.stats.DamageCalculation(casterData.stats.attack + damage));
                    target.GetComponent<CharacterData>().knockbackComponent.Knockdown(knockback);
                }
                break;
        }
    }
}
