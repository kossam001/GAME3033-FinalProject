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
    public Movement movementComponent;

    private float lookDirection;
    //public Rigidbody rigidbody;
    //public Animator animator;
    [SerializeField]
    private float forwardMagnitude;

    // Used to handle physics
    void FixedUpdate()
    {
        Turn();

        if (currentlyActiveSkill == null || currentlyActiveSkill.isStationary == false)
        {
            Move();
        }
        else
        {
            // Come to a stop if the skill is supposed to be stationary
            // rigidbody.velocity *= 0.97f;
        }
    }

    // Used to handle player input so it is not jittery
    private void Update()
    {
        Attack();
    }

    public void OnMovement(InputValue vector2)
    {
        Vector2 movementDirection = vector2.Get<Vector2>();

        Vector3 forwardForce = character.transform.forward * movementDirection.y;
        Vector3 rightForce = character.transform.right * movementDirection.x;
        movementComponent.Move(forwardForce + rightForce);

        //// Get movement based on character direction
        //Vector3 forwardForce = character.transform.forward * Input.GetAxis("Vertical");
        //Vector3 rightForce = character.transform.right * Input.GetAxis("Horizontal");

        ////// Sum forward and side force
        //Vector3 movementForce = forwardForce + rightForce;
        //rigidbody.AddForce(movementForce * movementSpeed * Time.deltaTime);

        ////// Clamp velocity
        //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, forwardMagnitude);

        ////character.transform.position = character.transform.position + movementForce * movementSpeed * Time.deltaTime;
    }

    public override void Turn()
    {
        // Change character orientation based on camera rotation
        //Quaternion rotation = Quaternion.RotateTowards(character.transform.rotation, cam.transform.rotation, rotationSpeed);
        //Vector3 euler = Vector3.Scale(rotation.eulerAngles, new Vector3(0, 1, 0));
        //character.transform.rotation = Quaternion.Euler(euler);
        //movementComponent.Turn(cam.transform.rotation);
    }

    public override IEnumerator UseSkill(ActiveSkill skill)
    {
        currentlyActiveSkill = skill;

        yield return StartCoroutine(currentlyActiveSkill.Use());
        currentlyActiveSkill = null;
    }

    public void Attack()
    {
        //if (Input.GetButtonDown("Fire1") && currentlyActiveSkill == null)
        //{
        //    StartCoroutine(UseSkill(skillsList[0]));
        //}
    }
}
