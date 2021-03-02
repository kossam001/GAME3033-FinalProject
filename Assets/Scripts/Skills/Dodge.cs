using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public float dodgeDuration = 0.3f;
    public float dodgeSpeed = 3;
    private string ownerTag;

    private void Awake()
    {
        ownerTag = tag;
    }

    public void TriggerDodge(Vector2 movementDirection, SkillController skillController, Movement movementComponent)
    {
        if (movementDirection.y != 0.0f || movementDirection.x != 0.0f)
        {
            if (skillController.isActive)
                skillController.Interrupt();

            GetComponent<Collider>().tag = "Untagged";

            Vector2 dodgeVector = movementDirection * dodgeSpeed;

            StartCoroutine(Dodging(dodgeVector, movementComponent));
        }
    }

    public IEnumerator Dodging(Vector2 movementDirection, Movement movementComponent)
    {
        float duration = dodgeDuration;

        while (duration >= 0.0f)
        {
            duration -= Time.deltaTime;

            Vector3 forwardForce = transform.forward * movementDirection.y;
            Vector3 rightForce = transform.right * movementDirection.x;

            movementComponent.Move(forwardForce + rightForce);

            yield return null;
        }

        GetComponent<Collider>().tag = ownerTag;
    }
}
