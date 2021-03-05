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
    [SerializeField] private Weapon weapon = null;
    public SkillController skillController;
    public Animator characterAnimator;
    public AnimatorOverrideController animatorOverride;
    public List<Socket> sockets; // If skill needs a collider, where is it
    private Dictionary<string, Socket> socketTable;

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

    public Skill getSkill(string skillName)
    {
        if (weapon.skills.skillTable.ContainsKey(skillName))
            return weapon.skills.skillTable[skillName];

        else
            return null;
    }

    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            other.gameObject.GetComponent<Weapon>().EquipWeapon(this);
        }
    }

    public Socket RetrieveSocket(string socketName)
    {
        return socketTable[socketName];
    }
}
