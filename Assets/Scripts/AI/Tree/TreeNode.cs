using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Root", menuName = "AITreeNodes/Root")]
public class TreeNode : ScriptableObject
{
    protected Brain brain;
    protected State state;
    public List<TreeNode> nodeTemplates;
    [SerializeField] protected List<TreeNode> nodes;

    public bool isSelector = false; // Otherwise it is a sequence

    [Tooltip("Chance of this node running.")]
    public float procRate = 1.0f;
    [Tooltip("Frequency of checks.")]
    public float procRateCheckFrequency = -1.0f;
    private float procTimer = 0.0f;

    public virtual void Initialize(Brain _brain, State _state)
    {
        // Each character will have its own nodes
        foreach (TreeNode node in nodeTemplates)
        {
            TreeNode nodeCopy = Instantiate(node);
            nodeCopy.Initialize(_brain, _state);
            nodes.Add(nodeCopy);
        }

        brain = _brain;
        state = _state;
    }

    public virtual bool PerformCheck()
    {
        return true;
    }

    public bool ProcCheck()
    {
        // Don't want to check
        if (procRateCheckFrequency == -1.0f) return true;

        procTimer -= Time.deltaTime;

        if (procTimer <= 0.0f)
        {
            procTimer = procRateCheckFrequency;

            if (Random.Range(0.0f, 1.0f) <= procRate)
                return true;
        }

        return false;
    }

    public virtual bool Run()
    {
        state.SetCurrentNode(this);

        foreach (TreeNode node in nodes)
        {
            if (!node.Run() ^ isSelector) return false;
        }
        return true;
    }
}
