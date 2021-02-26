using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private TreeNode rootNode;
    [SerializeField] private Brain brain;
    [Tooltip("Debug only")]
    [SerializeField] private TreeNode currentNode;

    private void Awake()
    {
        rootNode = Instantiate(rootNode);

        rootNode.Initialize(brain);
    }

    public void SetCurrentNode(TreeNode node)
    {
        currentNode = node;
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Run();
    }
}
