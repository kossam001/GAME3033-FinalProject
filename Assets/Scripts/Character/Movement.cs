using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");

    private Rigidbody rigidbody;
    private Animator animator;
    private CharacterData characterData;

    [SerializeField]
    public float maxWalkSpeed;
    public float maxRunSpeed;

    private float movementSpeed;
    public float rotationSpeed;
    public bool isRunning;
    public bool isStopped = false;

    private float LerpTimer = 0.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterData = GetComponent<CharacterData>();
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

    public void MovementCalculation(Vector2 movementDirection)
    {
        if ((characterData.skillController.canCancel || !characterData.skillController.SkillInUse()) && characterData.canMove)
        {
            characterData.skillController.CancelSkill();

            Vector3 forwardForce = transform.forward * movementDirection.y;
            Vector3 rightForce = transform.right * movementDirection.x;

            animator.SetFloat(MoveXHash, movementDirection.x);
            animator.SetFloat(MoveZHash, movementDirection.y);

            Move(forwardForce + rightForce);
        }
    }

    public void AIMove(Transform followTransform)
    {
        if ((characterData.skillController.canCancel || !characterData.skillController.SkillInUse()) && !isStopped)
        {
            characterData.skillController.CancelSkill();

            transform.position = followTransform.position;
            followTransform.rotation = Quaternion.Lerp(followTransform.rotation, transform.rotation, Time.deltaTime * 0.5f);
        }
        else
        {
            followTransform.position = transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, followTransform.rotation, Time.deltaTime * 0.5f);
        }
    }

    public void SetIsRunning(bool on)
    {
        isRunning = on;
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
        if (isStopped) return;

        rigidbody.velocity = Vector3.zero;
        isStopped = true;

        StartCoroutine(StopForDuration(duration));
    }

    private IEnumerator StopForDuration(float duration)
    {
        float originalRunSpeed = maxRunSpeed;
        float originalWalkSpeed = maxWalkSpeed;

        maxRunSpeed = 0.0f;
        maxWalkSpeed = 0.0f;

        yield return new WaitForSeconds(duration);

        isStopped = false;

        maxRunSpeed = originalRunSpeed;
        maxWalkSpeed = originalWalkSpeed;
    }
}
