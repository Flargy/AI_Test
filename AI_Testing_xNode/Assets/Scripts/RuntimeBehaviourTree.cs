﻿//Marcus Lundqvist
//Niclas Älmeby

using System.Collections.Generic;
using UnityEngine;
using XNode;

public class RuntimeBehaviourTree : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public BTGraph runtimeTree; // The behavior tree that will be activated during runtime
    BTRoot rootNode;
    bool isValid = false;

    /// <summary>
    /// Checks that the current behavior tree is valid and has a root node
    /// </summary>
    private void Start()
    {
        isValid = ValidateAndSetRootNode();
        if (isValid)
        {
            BehaviourTreeRuntimeData.RegisterBehaviourTree(this);
        }
    }

    /// <summary>
    /// Activates the behavior tree and returns its child nodes
    /// </summary>
    /// <param name="currentContextData"></param>
    public virtual void RunBehaviourTree(BTContextData currentContextData)
    {
        if (isValid)
        {

            BTNode runningNode;
            if (currentContextData.HasRunningNodes(out runningNode))
            {
                //Running node will attempt to get context from parent node
                //So we need to override the parents node context event if it's not executed
                BTNode parentNode = runningNode.GetPort("outResult").Connection.node as BTNode;
                if (parentNode != null)
                {
                    parentNode.context = currentContextData.owningContext;
                }
                runningNode.GetPort("outResult").GetOutputValue();
            }
            else
            {
                rootNode.context = currentContextData.owningContext;
                rootNode.GetInputValue("inResult", BTResult.FAILURE);
            }
        }
    }

    /// <summary>
    /// Tests a set of conditions to ensure that the behavior tree is functional
    /// </summary>
    /// <returns></returns>
    private bool ValidateAndSetRootNode()
    {
        if (runtimeTree == null)
        {
            Debug.LogWarning("runtimeTree is null - Behaviour tree will not run - Add a Behaviour Tree to the RuntimeBehaviourTree Script");
            return false;
        }

        List<BTRoot> rootNodeList = new List<BTRoot>();
        foreach (Node _node in runtimeTree.nodes)
        {
            BTRoot root = _node as BTRoot;
            if (root != null)
            {
                rootNodeList.Add(root);
            }
        }

        if (rootNodeList.Count != 1)
        {
            Debug.LogWarning("There is no root node or more than 1 root node in this behaviourTree - BehaviourTree will not run - Make sure there is exactly 1 BTRoot node in your graph");
            return false;
        }
        else rootNode = rootNodeList[0];

        return true;
    }
}
