using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Inventory/ItemEffects/Heal")]
public class Heal : ItemEffect
{
    public int healAmount = 20;

    public override void Activate(GameObject userCharacter)
    {
        userCharacter.GetComponentInChildren<CharacterData>().UpdateHealth(-healAmount);
    }
}
