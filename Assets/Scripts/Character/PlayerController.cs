using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    // Animator hashes
    private readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    public CharacterData characterData;

    private float lookDirection;

    public Vector2 movementDirection;

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
        //if (!characterData.canMove) return; // Lock dodging to one direction

        movementDirection = vector2.Get<Vector2>();
    }
    
    public void OnDodge(InputValue button)
    {
        if (characterData.skillController.GetActiveSkillName() == "Dodge") return;

        Skill selectedSkill = characterData.skills.skillTable["Dodge"];
        characterData.skillController.Use(selectedSkill, selectedSkill.overrideName);
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
