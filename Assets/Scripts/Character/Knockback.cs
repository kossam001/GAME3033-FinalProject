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
        if (!ResistanceCheck()) return;
        if (characterData.skillController.CanResistFlinch()) return;

        characterData.movementComponent.Stop(flinchClip.length);
        characterData.skillController.Stop(flinchClip.length);
        characterData.skillController.Interrupt();

        if (characterData.characterAnimator.GetBool(IsFlinchingHash))
            characterData.characterAnimator.Play("Flinch", 2, 0.0f);
        else
            characterData.characterAnimator.SetBool(IsFlinchingHash, true);

        characterData.GetComponent<Rigidbody>().AddForce(-characterData.gameObject.transform.forward * force);
    }

    public void Knockdown(float force)
    {
        if (!ResistanceCheck()) return;
        if (characterData.characterAnimator.GetBool(IsKnockedDownHash)) return;

        characterData.movementComponent.Stop(knockDownClip.length);
        characterData.skillController.Stop(knockDownClip.length);
        characterData.skillController.Interrupt();
        characterData.characterAnimator.SetBool(IsKnockedDownHash, true);

        characterData.GetComponent<Rigidbody>().AddForce(-characterData.gameObject.transform.forward * force);
    }

    // 
    public bool ResistanceCheck()
    {
        int knockbackStat = characterData.stats.currentKnockbackResistance;

        if (knockbackStat <= 0.0f)
        {
            characterData.stats.currentKnockbackResistance = characterData.stats.knockbackResistance;
            return true;
        }

        return false;
    }
}
