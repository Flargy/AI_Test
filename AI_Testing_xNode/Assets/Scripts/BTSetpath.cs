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
        if(!context.navAgent.pathPending && context.navAgent.enabled)
        {
            SetNewDestination();
        }

        return context.navAgent.hasPath || context.navAgent.pathPending ? BTResult.SUCCESS : BTResult.FAILURE;
    }

    private void SetNewDestination()
    {
        Vector3 movement = new Vector3(10, 0, 0);
        context.navAgent.SetDestination(context.navAgent.gameObject.transform.position + movement);
    }
}
