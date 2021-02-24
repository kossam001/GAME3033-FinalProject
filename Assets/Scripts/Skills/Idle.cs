using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MovementSkill
{
    public override IEnumerator Use()
    {
        // Idle does nothing
        yield return null;
    }
}
