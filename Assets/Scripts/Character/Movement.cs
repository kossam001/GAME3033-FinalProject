using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;

    [SerializeField]
    private float maxSpeed;
    public float movementSpeed;
    public float rotationSpeed;
    //public float stoppingVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movementForce)
    {
        Vector3 lookDirection = transform.forward;
        Vector3 movementDirection = rigidbody.velocity;

        float lookToMoveAngle = Vector3.Angle(lookDirection, movementDirection);
        Vector3 angleSign = Vector3.Cross(lookDirection, movementDirection);

        //// Move Backwards
        //if (lookToMoveAngle > 135 && Mathf.Abs(rigidbody.velocity.magnitude) > stoppingVelocity)
        //{
        //    animator.SetInteger("State", 4);
        //}
        //// Strafe Right
        //else if (lookToMoveAngle > 30 && angleSign.y > 0 && Mathf.Abs(rigidbody.velocity.magnitude) > stoppingVelocity)
        //{
        //    animator.SetInteger("State", 2);
        //}
        //else if (lookToMoveAngle > 30 && angleSign.y < 0 && Mathf.Abs(rigidbody.velocity.magnitude) > stoppingVelocity)
        //{
        //    animator.SetInteger("State", 3);
        //}
        //// Move Forward
        //else if (Vector3.Angle(lookDirection, movementDirection) <= 30 && Mathf.Abs(rigidbody.velocity.magnitude) > stoppingVelocity)
        //{
        //    animator.SetInteger("State", 1);
        //}
        //else
        //{
        //    rigidbody.velocity *= 0.97f;
        //    animator.SetInteger("State", 0);
        //}

        // Sum forward and side force
        rigidbody.AddForce(movementForce * movementSpeed * Time.deltaTime);

        // Clamp velocity
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    public void Turn(Quaternion rotateDirection)
    {
        // Change character orientation based on camera rotation
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed);
        Vector3 euler = Vector3.Scale(rotation.eulerAngles, new Vector3(0, 1, 0));
        transform.rotation = Quaternion.Euler(euler);
    }
}
