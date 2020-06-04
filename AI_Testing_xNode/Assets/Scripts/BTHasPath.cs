using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BTAgent(typeof(BTHasPath))]
public class BTHasPath : BTNode
{

    /// <summary>
    /// Checks if the AI agent currently has a path set and returns <see cref="BTResult.SUCCESS"/> if true and <see cref="BTResult.FAILURE"/> if false
    /// <para>Resets the path if the agent loses it without reaching the destination</para>
    /// </summary>
    /// <returns></returns>
    public override BTResult Execute()
    {


        if (context.agent.hasPath)
        {

            return BTResult.SUCCESS;
        }
        else if(DestinationIsInRange() || context.contextOwner.destination == Vector3.zero)
        {
           
            return BTResult.FAILURE;
        }

        context.agent.SetDestination(context.contextOwner.destination);

        return BTResult.SUCCESS;
     
    }

    public bool DestinationIsInRange()
    {
        return (context.contextOwner.destination - context.contextOwner.transform.position).sqrMagnitude < 2;
    }

}
