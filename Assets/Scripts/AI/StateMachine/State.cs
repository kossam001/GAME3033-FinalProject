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
    [SerializeField] protected TreeNode rootNode;
    [Tooltip("Debug only")]
    [SerializeField] private TreeNode currentNode;
    [SerializeField] private Brain brain; // Going to need to know when to change states

    public StateID id;
    public List<State> transferrableStates;

    protected Dictionary<StateID, State> transition;

    public void Initialize(Brain _brain)
    {
        rootNode = Instantiate(rootNode);
        rootNode.Initialize(_brain);

        brain = _brain;
    }

    public void SetCurrentNode(TreeNode node)
    {
        currentNode = node;
    }

    public virtual void Update()
    {
        rootNode.Run();
    }

    public virtual void ChangeState() { }
}
