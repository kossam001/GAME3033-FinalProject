using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "AITreeNodes/MoveToTarget")]
public class MoveToTarget : TreeNode
{
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");

    public override bool Run()
    {
        if (brain.agent.destination != brain.moveDestination)
        {
            CalcMovementAnimation();
            brain.agent.SetDestination(brain.moveDestination);
        }

        return base.Run();
    }

    public void CalcMovementAnimation()
    {
        Vector3 lookDirection = brain.character.transform.forward;
        Vector3 movementDirection = brain.agent.velocity;

        movementDirection = movementDirection.normalized;

        movementDirection = Quaternion.Euler(brain.character.transform.rotation.eulerAngles) * movementDirection;

        if (brain.agent.isStopped == true)
        {
            brain.character.GetComponent<Animator>().SetFloat(MoveXHash, 0.0f);
            brain.character.GetComponent<Animator>().SetFloat(MoveZHash, 0.0f);
        }
        else
        {
            brain.character.GetComponent<Animator>().SetFloat(MoveXHash, movementDirection.x);
            brain.character.GetComponent<Animator>().SetFloat(MoveZHash, movementDirection.z);
        }
    }
}
