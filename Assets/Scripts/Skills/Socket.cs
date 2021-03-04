using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject colliderObject;

    private void Awake()
    {
        colliderObject = transform.Find("Collider").gameObject;
    }
}
