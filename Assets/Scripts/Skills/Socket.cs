using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject colliderObject;
    public GameObject owner;

    private void Awake()
    {
        colliderObject = transform.Find("Collider").gameObject;
        colliderObject.GetComponent<DamageCollider>().owner = owner; 
    }
}
