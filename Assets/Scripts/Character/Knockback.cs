using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private readonly int IsFlinchingHash = Animator.StringToHash("IsFlinching");

    [SerializeField] private AnimationClip flinchClip;

    private CharacterData characterData;

    private void Awake()
    {
        characterData = GetComponent<CharacterData>();
    }

    public void Flinch()
    {
        if (characterData.characterAnimator.GetBool(IsFlinchingHash)) return;

        characterData.movementComponent.Stop(flinchClip.length);
        characterData.skillController.Interrupt();
        characterData.characterAnimator.SetBool(IsFlinchingHash, true);
    }
}
