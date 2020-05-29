using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTSetpath))]
public class BTSetpath : BTNode
{
    
    public override BTResult Execute()
    {
        if(!context.agent.pathPending && context.agent.enabled)
        {
            SetNewDestination();
        }

        return context.agent.hasPath || context.agent.pathPending ? BTResult.SUCCESS : BTResult.FAILURE;
    }

    private void SetNewDestination()
    {
        Vector3 movement = new Vector3(10, 0, 0);
        context.agent.SetDestination(context.agent.gameObject.transform.position + movement);
    }
}
