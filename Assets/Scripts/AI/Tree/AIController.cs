using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
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
}
