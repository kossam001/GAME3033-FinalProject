using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    // Animator hashes
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    public Movement movementComponent;
    public CharacterData characterData;

    private float lookDirection;

    public SkillList skills;
    public SkillController skillController;
    private Animator characterAnimator;

    [SerializeField] private float forwardMagnitude;

    private Vector2 movementDirection;

    public bool isShiftOn = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterAnimator = character.GetComponent<Animator>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f))
        {
            Turn();
        }

        if (skillController.canCancel || !skillController.SkillInUse())
        {
            skillController.CancelSkill();

            Vector3 forwardForce = character.transform.forward * movementDirection.y;
            Vector3 rightForce = character.transform.right * movementDirection.x;

            movementComponent.isRunning = isShiftOn;
            movementComponent.Move(forwardForce + rightForce);
        }
    }

    public void OnMovement(InputValue vector2)
    {
        movementDirection = vector2.Get<Vector2>();

        characterAnimator.SetFloat(MoveXHash, movementDirection.x);
        characterAnimator.SetFloat(MoveZHash, movementDirection.y);

        Invoke(nameof(InterruptAttack), 0.1f);
    }

    public void InterruptAttack()
    {
        if (isShiftOn && (movementDirection.y != 0.0f || movementDirection.x != 0.0f) && skillController.isActive)
            skillController.Interrupt();
    }

    public override void Turn()
    {
        movementComponent.Turn(cam.transform.rotation);
    }

    public override IEnumerator UseSkill(ActiveSkill skill)
    {
        currentlyActiveSkill = skill;

        yield return StartCoroutine(currentlyActiveSkill.Use());
        currentlyActiveSkill = null;
    }

    public void OnAttack(InputValue button)
    {
        Skill selectedSkill = skills.skillTable["BasicAttack1"];

        skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public void OnShift(InputValue button)
    {
        isShiftOn = button.isPressed;
        characterAnimator.SetBool(IsRunningHash, button.isPressed);
    }
}
