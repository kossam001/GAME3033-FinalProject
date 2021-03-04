using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    private readonly int IsDeadHash = Animator.StringToHash("IsDead");

    public int id;
    public Team team;

    public StateMachine stateMachine;
    public Movement movementComponent;
    public Knockback knockbackComponent;

    [Header("Skills")]
    public SkillList skills;
    public SkillController skillController;
    public Animator characterAnimator;

    public int health = 100;
    public int currentHealth;

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

        if (currentHealth <= 0.0f)
        {
            characterAnimator.SetBool(IsDeadHash, true);
            StageManager.Instance.RemoveFromTeam(this);

            if (stateMachine != null)
                stateMachine.enabled = false;
        }
    }
}
