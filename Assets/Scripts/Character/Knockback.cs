using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private readonly int IsFlinchingHash = Animator.StringToHash("IsFlinching");
    private readonly int IsKnockedDownHash = Animator.StringToHash("IsKnockedDown");

    [SerializeField] private AnimationClip flinchClip;
    [SerializeField] private AnimationClip knockDownClip;

    private CharacterData characterData;

    private void Awake()
    {
        characterData = GetComponent<CharacterData>();
    }

    public void Flinch(float force)
    {
        if (characterData.characterAnimator.GetBool(IsFlinchingHash)) return;
        if (characterData.skillController.CanResistFlinch()) return;

        characterData.movementComponent.Stop(flinchClip.length);
        characterData.skillController.Stop(flinchClip.length);
        characterData.skillController.Interrupt();
        characterData.characterAnimator.SetBool(IsFlinchingHash, true);

        characterData.GetComponent<Rigidbody>().AddForce(-characterData.gameObject.transform.forward * force);
    }

    public void Knockdown(float force)
    {
        if (characterData.characterAnimator.GetBool(IsKnockedDownHash)) return;

        characterData.movementComponent.Stop(knockDownClip.length);
        characterData.skillController.Stop(knockDownClip.length);
        characterData.skillController.Interrupt();
        characterData.characterAnimator.SetBool(IsKnockedDownHash, true);

        characterData.GetComponent<Rigidbody>().AddForce(-characterData.gameObject.transform.forward * force);
    }
}
