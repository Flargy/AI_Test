using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[BTAgent(typeof(BTSelectHidingSpot))]
public class BTSelectHidingSpot : BTNode
{
    
    public override BTResult Execute()
    {
        if (context.agent.pathPending == false && context.agent.hasPath == false)
        {
            HidingSpot spot = PlaceCreator.Intance.GetNextHidingSpot();
            context.contextOwner.destination = spot.transform.position;
            context.agent.SetDestination(spot.transform.position);
            Debug.Log("getting new spot " + spot.name);
            //context.agent.SetDestination(targetSpot.position);
            //context.contextOwner.destination = targetSpot.position;

            return BTResult.SUCCESS;
        }
        return BTResult.FAILURE;
    }
}