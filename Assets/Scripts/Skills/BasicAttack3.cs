using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack3", menuName = "Skills/BasicAttack3")]
public class BasicAttack3 : BasicAttack1
{
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        switch (caster.tag)
        {
            case "Ally":
                if (target.CompareTag("Enemy"))
                {
                    target.GetComponent<CharacterData>().UpdateHealth(damage);
                    target.GetComponent<CharacterData>().knockbackComponent.Knockdown();
                }
                break;
            case "Enemy":
                if (target.CompareTag("Ally"))
                {
                    target.GetComponent<CharacterData>().UpdateHealth(damage);
                    target.GetComponent<CharacterData>().knockbackComponent.Knockdown();
                }
                break;
        }
    }
}
