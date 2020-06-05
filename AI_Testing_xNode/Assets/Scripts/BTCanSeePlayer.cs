//Marcus Lundqvist
//Niclas Älmeby

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[BTAgent(typeof(BTCanSeePlayer))]
public class BTCanSeePlayer : BTNode
{

    /// <summary>
    /// Checks the value of <see cref="AIComponent.playerFound"/> and returns <see cref="BTResult.SUCCESS"/> if true and <see cref="BTResult.FAILURE"/> if false 
    /// <para>This is used to determine if the AI agent is capable of seeing the player</para>
    /// </summary>
    /// <returns></returns>
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

        
    }

    
}
