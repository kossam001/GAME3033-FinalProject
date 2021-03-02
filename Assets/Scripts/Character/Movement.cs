using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;

    [SerializeField]
    public float maxWalkSpeed;
    public float maxRunSpeed;

    private float movementSpeed;
    public float rotationSpeed;
    public bool isRunning;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movementForce)
    {
        movementSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;

        Vector3 lookDirection = transform.forward;
        Vector3 movementDirection = rigidbody.velocity;

        float lookToMoveAngle = Vector3.Angle(lookDirection, movementDirection);
        Vector3 angleSign = Vector3.Cross(lookDirection, movementDirection);

        // Sum forward and side force
        rigidbody.AddForce(movementForce * movementSpeed * Time.deltaTime);
    }

    public void Turn(Quaternion rotateDirection)
    {
        // Change character orientation based on camera rotation
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed);
        Vector3 euler = Vector3.Scale(rotation.eulerAngles, new Vector3(0, 1, 0));
        transform.rotation = Quaternion.Euler(euler);
    }

    public void Stop(float duration)
    {
        rigidbody.velocity = Vector3.zero;

        StartCoroutine(StopForDuration(duration));
    }

    private IEnumerator StopForDuration(float duration)
    {
        float originalRunSpeed = maxRunSpeed;
        float originalWalkSpeed = maxWalkSpeed;

        maxRunSpeed = 0.0f;
        maxWalkSpeed = 0.0f;

        yield return new WaitForSeconds(duration);

        maxRunSpeed = originalRunSpeed;
        maxWalkSpeed = originalWalkSpeed;
    }
}
