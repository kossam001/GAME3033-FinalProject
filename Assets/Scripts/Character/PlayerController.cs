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
    public CharacterData characterData;

    private float lookDirection;

    private Vector2 movementDirection;

    public bool isShiftOn = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f))
        {
            Turn();
        }

        characterData.movementComponent.MovementCalculation(movementDirection);
        characterData.movementComponent.SetIsRunning(isShiftOn);
    }

    public void OnMovement(InputValue vector2)
    {
        movementDirection = vector2.Get<Vector2>();

        if (!characterData.canMove) return; // Lock dodging to one direction

        characterData.characterAnimator.SetFloat(MoveXHash, movementDirection.x);
        characterData.characterAnimator.SetFloat(MoveZHash, movementDirection.y);
    }
    
    public void OnDodge(InputValue button)
    {
        characterData.dodgeComponent.TriggerDodge(movementDirection, characterData.skillController, characterData.movementComponent);
    }

    public override void Turn()
    {
        characterData.movementComponent.Turn(cam.transform.rotation);
    }

    public override IEnumerator UseSkill(ActiveSkill skill)
    {
        currentlyActiveSkill = skill;

        yield return StartCoroutine(currentlyActiveSkill.Use());
        currentlyActiveSkill = null;
    }

    public void OnAttack(InputValue button)
    {
        Skill selectedSkill = characterData.skills.skillTable["BasicAttack1"];

        characterData.skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public void OnShift(InputValue button)
    {
        isShiftOn = button.isPressed;
        characterData.characterAnimator.SetBool(IsRunningHash, button.isPressed);
    }
}
