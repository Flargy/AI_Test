using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

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

            foreach (NodePort _port in connections)
            {
                BTResult result = (BTResult)_port.GetOutputValue();
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
