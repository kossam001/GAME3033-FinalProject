using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");
    private readonly int IsDodgingHash = Animator.StringToHash("IsDodging");
    private readonly int DodgeSpeedHash = Animator.StringToHash("DodgeSpeed");

    public float dodgeDuration = 0.3f;
    public float dodgeSpeed = 3;
    private string ownerTag;

    private CharacterData characterData;
    private Animator animator;
    [SerializeField] private AnimationClip clip;

    private void Awake()
    {
        ownerTag = tag;
        animator = GetComponent<Animator>();
        characterData = GetComponent<CharacterData>();
    }

    public void TriggerDodge(Vector2 movementDirection, SkillController skillController, Movement movementComponent)
    {
        if (!characterData.canMove) return;

        if (skillController.isActive)
                skillController.Interrupt();

        GetComponent<Collider>().tag = "Untagged";

        characterData.canMove = false;

        animator.SetFloat(MoveXHash, movementDirection.x);
        animator.SetFloat(MoveZHash, movementDirection.y);
        animator.SetBool(IsDodgingHash, true);

        Invoke(nameof(Stop), clip.length / animator.GetFloat(DodgeSpeedHash));
    }

    private void Stop()
    {
        animator.SetBool(IsDodgingHash, false);
        GetComponent<Collider>().tag = ownerTag;
        characterData.canMove = true;
    }
}
