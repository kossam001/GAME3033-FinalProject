using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CallAlly", menuName = "Inventory/ItemEffects/CallAlly")]
public class CallAlly : ItemEffect
{
    [SerializeField] private GameObject ally;

    public override void Activate(GameObject userCharacter)
    {
        StageManager.Instance.SpawnAlly(ally);
    }
}
