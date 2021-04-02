using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageCollider : MonoBehaviour
{
    public int damage = 5;
    public GameObject owner;
    public UnityAction<GameObject, GameObject> action;

    private void OnTriggerEnter(Collider other)
    {
        UseEffect(action, other.gameObject, owner);
    }

    public void UseEffect(UnityAction<GameObject, GameObject> effect, GameObject target, GameObject caster)
    {
        if (effect != null && target != null && caster != null)
            effect(target, caster);
    }
}
