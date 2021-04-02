using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "CharacterStat/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public int health = 100;
    public int currentHealth;

    public int defense = 0;
    public int currentDefense;

    public int attack = 0;
    public int currentAttack;

    [Tooltip("Ability to resist knockback.")]
    public int knockbackResistance = 0;
    public int currentKnockbackResistance = 0;


    public void InitializeStats()
    {
        currentHealth = health;

        currentAttack = attack;

        currentDefense = defense;

        currentKnockbackResistance = knockbackResistance;
    }

    public int DamageCalculation(int attackValue)
    {
        // Decrease knockback resistance
        currentKnockbackResistance -= attackValue;

        return attackValue - defense;
    }
}
