using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateID
{
    InCombat,
    Chase
}

public class State : ScriptableObject
{
    public TreeNode rootNode;
    public StateID id;
    public List<State> transferrableStates;

    protected Dictionary<StateID, State> transition;

    public void Update() { }
    public void ChangeState() { }
}
