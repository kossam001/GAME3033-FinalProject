using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform billboardTransform;
    public Transform target;

    private void LateUpdate()
    {
        billboardTransform.LookAt(target, -Vector3.up);
    }
}
