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
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");
    private readonly int ComboHash = Animator.StringToHash("Combo");
    private readonly int ComboEndHash = Animator.StringToHash("ComboEnd");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    public Movement movementComponent;

    private float lookDirection;
    private Animator characterAnimator;
    [SerializeField] private float forwardMagnitude;

    private Vector2 movementDirection;

    private void Awake()
    {
        characterAnimator = character.GetComponent<Animator>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if (movementDirection.y != 0.0f || movementDirection.x != 0.0f)
        {
            Vector3 forwardForce = character.transform.forward * movementDirection.y;
            Vector3 rightForce = character.transform.right * movementDirection.x;
            movementComponent.Move(forwardForce + rightForce);

            Turn();
        }
    }

    public void OnMovement(InputValue vector2)
    {
        movementDirection = vector2.Get<Vector2>();

        characterAnimator.SetFloat(MoveXHash, movementDirection.x);
        characterAnimator.SetFloat(MoveZHash, movementDirection.y);
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
        int currentCombo = characterAnimator.GetInteger(ComboHash);
        bool currentlyAttacking = characterAnimator.GetBool(IsAttackingHash);

        if (button.isPressed && !currentlyAttacking)
        {
            characterAnimator.SetBool(ComboEndHash, false);
            characterAnimator.SetBool(IsAttackingHash, true);
            characterAnimator.SetInteger(ComboHash, ++currentCombo);
        }
        //if (Input.GetButtonDown("Fire1") && currentlyActiveSkill == null)
        //{
        //    StartCoroutine(UseSkill(skillsList[0]));
        //}
    }
}
