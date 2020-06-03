using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BTAgent(typeof(BTHasPath))]
public class BTHasPath : BTNode
{

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
