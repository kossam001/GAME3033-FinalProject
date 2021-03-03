using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public Team team;

    public Movement movementComponent;
    public Knockback knockbackComponent;

    [Header("Skills")]
    public SkillList skills;
    public SkillController skillController;
    public Animator characterAnimator;

    public int health = 100;
    private int currentHealth;

    public bool canMove = true;

    [SerializeField] private Slider healthIndicator;

    private void Awake()
    {
        currentHealth = health;
        movementComponent = GetComponent<Movement>();
        knockbackComponent = GetComponent<Knockback>();
        characterAnimator = GetComponent<Animator>();
    }

    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        healthIndicator.value = (float)currentHealth / (float)health;
    }
}
