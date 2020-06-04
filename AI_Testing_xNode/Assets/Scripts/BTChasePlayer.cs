using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


[BTAgent(typeof(BTChasePlayer))]
public class BTChasePlayer : BTNode
{

    /// <summary>
    /// Sets the new destination of the AI agent to the last known position of the player
    /// </summary>
    /// <returns></returns>
    public override BTResult Execute()
    {
        
        context.agent.SetDestination(context.contextOwner.lastKnownPosition);

        return BTResult.SUCCESS;

    }
}
