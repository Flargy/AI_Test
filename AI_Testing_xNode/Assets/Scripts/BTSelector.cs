//Marcus Lundqvist
//Niclas Älmeby

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// Can be connected to multiple nodes in a behavior tree and only fully activates the first one that returns <see cref="BTResult.SUCCESS"/>
/// </summary>
[BTComposite(typeof(BTSelector))]
public class BTSelector : BTNode
{
    [Input] public List<BTResult> inResults;

    public override BTResult Execute()
    {

        NodePort inPort = GetPort("inResults");
        if (inPort != null)
        {
            List<NodePort> connections = inPort.GetConnections();

            foreach (NodePort port in connections) // Goes through each connected node and explores them until one returns success
            {
                BTResult result = (BTResult)port.GetOutputValue();
                if (result == BTResult.SUCCESS)
                {
                    return BTResult.SUCCESS;
                }
                if (result == BTResult.RUNNING)
                {
                    return BTResult.RUNNING;
                }
            }
            return BTResult.FAILURE;
        }
        else return BTResult.FAILURE;
    }
}
