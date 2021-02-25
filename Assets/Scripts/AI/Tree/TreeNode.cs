using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject
{
    protected Brain brain;
    public List<TreeNode> nodes;

    public bool isSelector = false; // Otherwise it is a sequence

    public virtual bool PerformCheck()
    {
        return true;
    }

    public virtual void Initialize(Brain _brain)
    {
        brain = _brain;
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
