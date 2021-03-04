using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private Movement movementController;
    public Transform followTarget;

    public SkillController skillController;

    private void Awake()
    {
        movementController = GetComponent<Movement>();
    }

    private void Update()
    {
        movementController.AIMove(followTarget);
    }

    public void UseSkill(Skill selectedSkill)
    {
        skillController.Use(selectedSkill, selectedSkill.overrideName);
    }

    public void CancelSkill()
    {
        skillController.CancelSkill();
    }

    public void SetRun(bool on, NavMeshAgent agent)
    {
        movementController.SetIsRunning(on);

        if (on)
        {
            agent.speed = runSpeed;
        }

        else
        {
            agent.speed = walkSpeed;
        }
    }
}
