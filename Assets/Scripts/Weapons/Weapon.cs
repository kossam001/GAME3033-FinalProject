using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SkillList skills;
    public CharacterData character;

    [Header("Animation")]
    [Tooltip("Overrides animation clip in animator.  Uses a parallel list.")]
    public List<AnimationClip> animations;
    public List<string> overrideNames;
    public Dictionary<string, AnimationClip> animationTable;

    [Header("Collider Transform")]
    public Vector3 colliderPosition;
    public Vector3 colliderRotation;
    public Vector3 colliderScale;

    private void Awake()
    {
        for (int i = 0; i < animations.Count - 1; i++)
            animationTable.Add(overrideNames[i], animations[i]);
    }

    public void SetWeapon()
    {
        List<string> overrideNames = new List<string>(animationTable.Keys);

        foreach (string name in overrideNames)
            character.animatorOverride[name] = animationTable[name];

        Transform colliderTransform = transform.parent.Find("Collider");
        colliderTransform.position = colliderPosition;
        colliderTransform.rotation = Quaternion.Euler(colliderRotation);
        colliderTransform.localScale = colliderScale;
    }
}
