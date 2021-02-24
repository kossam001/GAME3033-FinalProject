using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<ActiveSkill> skillsList;
    public ActiveSkill currentlyActiveSkill;

    public virtual void Move() { }
    public virtual void Turn() { }

    // TODO: Add restrictions to skill usage
    public virtual IEnumerator UseSkill(ActiveSkill skill) { yield return null; }
    public virtual void Jump() { }
}
