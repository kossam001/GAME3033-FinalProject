using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject
{
    private Brain brain;
    public List<TreeNode> nodes;

    public void Initialize(Brain _brain)
    {
        brain = _brain;
    }

    public bool Run()
    {
        foreach (TreeNode node in nodes)
        {
            return node.Run();
        }
        return false;
    }
}
