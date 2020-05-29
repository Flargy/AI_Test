﻿using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
public abstract class BTNode : Node
{
    [Output] public BTResult outResult;
    public string nodeDescription = "";

    public abstract BTResult Execute();

    internal BTContext context;

    public virtual void OnStart() { }

    public override object GetValue(NodePort port)
    {
        BTNode parentNode = port.Connection.node as BTNode;

        if(parentNode != null)
        {
            context = parentNode.context;
        }

       

#if UNITY_EDITOR
        context.behaviourHistory.Add(nodeDescription == "" ? GetType().ToString() : nodeDescription);
#endif
        return Execute();
        
    }
}
