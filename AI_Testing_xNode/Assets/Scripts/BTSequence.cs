//Marcus Lundqvist
//Niclas Älmeby

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// Can be connected to multiple nodes in a behavior tree and will continue activating them in sequence according to the <see cref="inResults"/> order as long as they return <see cref="BTResult.SUCCESS"/>
/// </summary>
[BTComposite(typeof(BTSequence))]
public class BTSequence : BTNode
{
    [Input] public List<BTResult> inResults;

    public override BTResult Execute()
    {
        NodePort inPort = GetPort("inResults");

        if(inPort != null)
        {
            List<NodePort> connections = inPort.GetConnections();

            foreach (NodePort port in connections) // Goes through each connected node until one returns failure
            {
                BTResult result = (BTResult)port.GetOutputValue();
                if(result == BTResult.FAILURE)
                {
                    return BTResult.FAILURE;
                }
                if(result == BTResult.RUNNING)
                {
                    return BTResult.RUNNING;
                }
            }
            return BTResult.SUCCESS;
        }
        else
        {
            return BTResult.FAILURE;
        }
    }
}
