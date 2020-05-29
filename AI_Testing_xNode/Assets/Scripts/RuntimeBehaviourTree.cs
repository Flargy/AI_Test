using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class RuntimeBehaviourTree : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public BTGraph runtimeTree;
    BTRoot rootNode;
    bool isValid = false;

    private void Start()
    {
        isValid = ValidateAndSetRoot();
        if(isValid == true)
        {
            BehaviourTreeRuntimeData.RegisterBehaviourTree(this);
        }
    }

    public virtual void RunBehaviourTree(BTContextData _currentContextData)
    {
        if (isValid)
        {
            BTNode runningNode;
            if(_currentContextData.HasRunningNodes(out runningNode))
            {
                BTNode parentNode = runningNode.GetPort("outResult").Connection.node as BTNode;
                if(parentNode != null)
                {
                    parentNode.context = _currentContextData.owningContext;
                }
                runningNode.GetPort("outResult").GetOutputValue();
            }
            else
            {
                rootNode.context = _currentContextData.owningContext;
                rootNode.GetInputValue("inResult", BTResult.FAILURE);
            }
        }
    }

    private bool ValidateAndSetRoot()
    {
        if(runtimeTree == null)
        {
            return false;
        }

        List<BTRoot> rootNodeList = new List<BTRoot>();
        foreach (Node _node in runtimeTree.nodes)
        {
            BTRoot root = _node as BTRoot;
            if(root != null)
            {
                rootNodeList.Add(root);
            }
        }

        if (rootNodeList.Count != 1)
        {
            return false;
        }

        else rootNode = rootNodeList[0];
        return true;
    }
}
