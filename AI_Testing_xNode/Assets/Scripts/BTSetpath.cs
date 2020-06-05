//Marcus Lundqvist
//Niclas Älmeby

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Node that sets a new destination for the <see cref="NavMeshAgent"/>
/// <para>OBS: Test class that is not used</para>
/// </summary>
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
        Vector3 movement = new Vector3(15, 0, 0);
        context.agent.SetDestination(context.agent.gameObject.transform.position + movement);
    }
}
