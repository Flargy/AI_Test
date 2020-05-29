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
        int layerMask = 1 << 0;
        RaycastHit hit;


        if(Physics.Raycast(context.transform.position, context.contextOwner.target.transform.position - context.transform.position, out hit, 20, layerMask))
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
