using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Movement movementController;

    private void Awake()
    {
        movementController = GetComponent<Movement>();
    }
}
