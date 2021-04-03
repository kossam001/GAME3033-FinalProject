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
    public Interact interactComponent;

    [Header("Skills")]
    [SerializeField] private Weapon weapon = null;
    public SkillController skillController;
    public Animator characterAnimator;
    public AnimatorOverrideController animatorOverride;
    public List<Socket> sockets; // If skill needs a collider, where is it
    private Dictionary<string, Socket> socketTable;

    public CharacterStats stats;

    public bool canMove = true;

    [SerializeField] private Slider healthIndicator;

    private void Awake()
    {
        stats = Instantiate(stats);
        stats.InitializeStats();

        movementComponent = GetComponent<Movement>();
        knockbackComponent = GetComponent<Knockback>();
        characterAnimator = GetComponent<Animator>();

        animatorOverride = Instantiate(animatorOverride);
        characterAnimator.runtimeAnimatorController = animatorOverride;

        socketTable = new Dictionary<string, Socket>();

        foreach (Socket socket in sockets)
        {
            socketTable.Add(socket.name, socket);
        }
    }

    public void UpdateHealth(int damage)
    {
        stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, 100);


        healthIndicator.value = (float)stats.currentHealth / (float)stats.health;

        if (stats.currentHealth <= 0.0f)
        {
            characterAnimator.SetBool(IsDeadHash, true);
            StageManager.Instance.RemoveFromTeam(this);

            if (stateMachine != null)
                stateMachine.enabled = false;
        }
    }

    public Skill getSkill(SkillID skillID)
    {
        if (weapon.skills.skillTable.ContainsKey(skillID))
            return weapon.skills.skillTable[skillID];

        else
            return null;
    }

    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public Socket RetrieveSocket(string socketName)
    {
        return socketTable[socketName];
    }
}
