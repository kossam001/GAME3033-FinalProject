using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public Movement movementComponent;
    public Dodge dodgeComponent;
    public Knockback knockbackComponent;

    [Header("Skills")]
    public SkillList skills;
    public SkillController skillController;
    public Animator characterAnimator;

    public int health = 100;
    private int currentHealth;

    [SerializeField] private Slider healthIndicator;

    private void Awake()
    {
        currentHealth = health;
        movementComponent = GetComponent<Movement>();
        dodgeComponent = GetComponent<Dodge>();
        knockbackComponent = GetComponent<Knockback>();
        characterAnimator = GetComponent<Animator>();
    }

    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        healthIndicator.value = (float)currentHealth / (float)health;
    }
}
