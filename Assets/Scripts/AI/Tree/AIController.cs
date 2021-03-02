using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Movement movementController;
    public Transform followTarget;

    private void Awake()
    {
        movementController = GetComponent<Movement>();
    }

    private void Update()
    {
        //Vector3 direction = followTarget.position - transform.position;
        //float distance = Vector3.Distance(followTarget.position, transform.position);
        //Debug.Log(distance);

        //// Player controller uses x and y instead of x and z
        //Vector2 direction2 = new Vector2(direction.x, -direction.z);
        //movementController.Move(direction);

        movementController.AIMove(followTarget);

        //if (Vector3.Distance(followTarget.position, transform.position) > 0.0f)
        //{
        //    followTarget.position = transform.position;
        //}
    }
}
