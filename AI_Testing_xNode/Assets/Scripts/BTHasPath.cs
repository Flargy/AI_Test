//Marcus Lundqvist
//Niclas Älmeby

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


        if (context.agent.hasPath) // Checks if the agent currently has a path
        {

            return BTResult.SUCCESS;
        }
        else if(DestinationIsInRange() || context.contextOwner.destination == Vector3.zero) // If the agent doesn't have a path and is within reach of the previous path do this
        {
           
            return BTResult.FAILURE;
        }

        context.agent.SetDestination(context.contextOwner.destination); // set the path again if the previous conditions both fail, the agent can occasionally lose its path iwthout reaching it

        return BTResult.SUCCESS;
     
    }

    public bool DestinationIsInRange()
    {
        return (context.contextOwner.destination - context.contextOwner.transform.position).sqrMagnitude < 2;
    }

}
