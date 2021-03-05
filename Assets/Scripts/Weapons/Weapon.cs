using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    public SkillList skills;

    [Header("Animation")]
    [Tooltip("Overrides animation clip in animator.  Uses a parallel list.")]
    public List<AnimationClip> animations;
    public List<string> overrideNames;
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

        for (int i = 0; i < animations.Count - 1; i++)
            animationTable.Add(overrideNames[i], animations[i]);
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
        colliderTransform.position = colliderPosition;
        colliderTransform.rotation = Quaternion.Euler(colliderRotation);
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
