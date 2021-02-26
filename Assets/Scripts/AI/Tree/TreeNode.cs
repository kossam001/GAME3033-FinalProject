using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject
{
    protected Brain brain;
    public List<TreeNode> nodeTemplates;
    [SerializeField] protected List<TreeNode> nodes;

    public bool isSelector = false; // Otherwise it is a sequence

    public virtual void Initialize(Brain _brain)
    {
        // Each character will have its own nodes
        foreach (TreeNode node in nodeTemplates)
        {
            TreeNode nodeCopy = Instantiate(node);
            nodeCopy.Initialize(_brain);
            nodes.Add(nodeCopy);
        }

        brain = _brain;
    }

    public virtual bool PerformCheck()
    {
        return true;
    }

    public virtual bool Run()
    {
        foreach (TreeNode node in nodes)
        {
            if (!node.Run() ^ isSelector) return false;
        }
        return true;
    }
}
