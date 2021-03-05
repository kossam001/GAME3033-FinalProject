using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    public SkillList skills;

    [Header("Animation")]
    [Header("Walk Animations")]
    public List<AnimationClip> walkAnimations;
    public List<string> walkOverrideNames;

    [Header("Run Animations")]
    public List<AnimationClip> runAnimations;
    public List<string> runOverrideNames;

    [Header("Evade Animations")]
    public List<AnimationClip> evadeAnimations;
    public List<string> evadeOverrideNames;

    [Header("Other Animations")]
    public List<AnimationClip> otherAnimations;
    public List<string> otherOverrideNames;

    public Dictionary<string, AnimationClip> animationTable;

    [Header("Collider Transform")]
    public Vector3 colliderPosition;
    public Vector3 colliderRotation;
    public Vector3 colliderScale;

    [Header("Weapon Transform")]
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    private void Awake()
    {
        position = transform.localPosition;
        rotation = transform.localRotation.eulerAngles;
        scale = transform.localScale;

        animationTable = new Dictionary<string, AnimationClip>();

        for (int i = 0; i < walkAnimations.Count - 1; i++)
            animationTable.Add(walkOverrideNames[i], walkAnimations[i]);

        for (int i = 0; i < runAnimations.Count - 1; i++)
            animationTable.Add(runOverrideNames[i], runAnimations[i]);

        for (int i = 0; i < evadeAnimations.Count - 1; i++)
            animationTable.Add(evadeOverrideNames[i], evadeAnimations[i]);

        for (int i = 0; i < otherAnimations.Count - 1; i++)
            animationTable.Add(otherOverrideNames[i], otherAnimations[i]);
    }

    public void EquipWeapon(CharacterData _character)
    {
        DropWeapon(_character);

        List<string> overrideNames = new List<string>(animationTable.Keys);

        foreach (string name in overrideNames)
            _character.animatorOverride[name] = animationTable[name];

        // Disable physics
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        // Attach to socket
        Socket socket = _character.RetrieveSocket("WeaponSocket");
        
        transform.parent.SetParent(socket.transform); // A fix for the weapon offset

        transform.parent.localPosition = Vector3.zero;
        transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
        transform.parent.localScale = new Vector3(1, 1, 1);

        transform.localPosition = position;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        transform.localScale = scale;

        // Change collider size
        Transform colliderTransform = socket.colliderObject.transform;
        colliderTransform.localPosition = colliderPosition;
        colliderTransform.localRotation = Quaternion.Euler(colliderRotation);
        colliderTransform.localScale = colliderScale;

        // Replace previous weapon's references
        _character.SetWeapon(this);
    }

    public void DropWeapon(CharacterData _character)
    {
        Weapon weapon = _character.GetWeapon();
        if (weapon == null) return;
            
        weapon.GetComponent<BoxCollider>().enabled = true;
        weapon.GetComponent<Rigidbody>().isKinematic = false;

        weapon.transform.parent.SetParent(null);
        _character.SetWeapon(null);
    }

    public override void Use(CharacterData _character)
    {
        EquipWeapon(_character);
    }
}
