using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public Vector3 moveDestination;

    public List<GameObject> enemyTarget;
    private GameObject activeTarget;
}
