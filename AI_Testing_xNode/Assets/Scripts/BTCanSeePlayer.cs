using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTCanSeePlayer))]
public class BTCanSeePlayer : BTNode
{

    public override BTResult Execute()
    {
        if(context.contextOwner.playerFound == true)
        {
            return BTResult.SUCCESS;
        }
        else
        {
            return BTResult.FAILURE;
        }

        //return context.navAgent.hasPath || context.navAgent.pathPending ? BTResult.SUCCESS : BTResult.FAILURE;
    }

    
}
