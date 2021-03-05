using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
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
            Turn();

        characterData.movementComponent.MovementCalculation(movementDirection);
        characterData.movementComponent.SetIsRunning(isShiftOn);
    }

    public void OnMovement(InputValue vector2)
    {
        movementDirection = vector2.Get<Vector2>();
    }
    
    public void OnDodge(InputValue button)
    {
        if (characterData.skillController.GetActiveSkillName() == "Dodge") return;

        Skill selectedSkill = characterData.getSkill("Dodge");
        characterData.skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public override void Turn()
    {
        characterData.movementComponent.Turn(cam.transform.rotation);
    }

    public void OnAttack(InputValue button)
    {
        Skill selectedSkill;

        if (isShiftOn)
        {
            selectedSkill = characterData.getSkill("JumpAttack");
        }
        else
        {
            selectedSkill = characterData.getSkill("BasicAttack1");
        }

        characterData.skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public void OnShift(InputValue button)
    {
        isShiftOn = button.isPressed;
    }

    public void OnAltAttack(InputValue button)
    {
        Skill selectedSkill;

        if (isShiftOn)
        {
            selectedSkill = characterData.getSkill("SpinAttack");
        }
        else
        {
            selectedSkill = characterData.getSkill("Kick");
        }

        characterData.skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public void OnInteract(InputValue button)
    {
        if (button.isPressed)
            characterData.interactComponent.Use();
    }
}
