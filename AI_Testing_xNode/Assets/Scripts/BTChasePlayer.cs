using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


[BTAgent(typeof(BTChasePlayer))]
public class BTChasePlayer : BTNode
{
    public override BTResult Execute()
    {
        
        context.agent.SetDestination(context.contextOwner.lastKnownPosition);

        return BTResult.SUCCESS;

    }
}
