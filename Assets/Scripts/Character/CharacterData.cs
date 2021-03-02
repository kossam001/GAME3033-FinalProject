using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public int health = 100;
    private int currentHealth;

    [SerializeField] private Slider healthIndicator;

    private void Awake()
    {
        currentHealth = health;
    }

    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        healthIndicator.value = (float)currentHealth / (float)health;
    }
}
