using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private TreeNode rootNode;
    [SerializeField] private Brain brain;

    private void Awake()
    {
        rootNode.Initialize(brain);
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Run();
    }
}
