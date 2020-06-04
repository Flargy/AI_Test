using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// The root node of the behavior trees. Wait for a return value to be passed from the behavior tree
/// </summary>
[BTComposite(typeof(BTRoot))]
public class BTRoot : BTNode
{
    [Input] public BTResult inResult;

    public override object GetValue(NodePort port)
    {
        return Execute();
    }

    public override BTResult Execute()
    {
        return GetInputValue("inResult", BTResult.FAILURE);
    }
}
