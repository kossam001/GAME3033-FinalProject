using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kick", menuName = "Skills/Kick")]
public class Kick : BasicAttack1
{
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
