//Marcus Lundqvist
//Niclas Älmeby

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Sets the speed of the <see cref="NavMeshAgent"/> through <see cref="AIComponent.navAgent"/>
/// </summary>
[BTAgent(typeof(BTSetAgentSpeed))]
public class BTSetAgentSpeed : BTNode
{
    public float desiredSpeed;
    public override BTResult Execute()
    {
        context.agent.speed = desiredSpeed; // Changes the speed of the agent
        return BTResult.SUCCESS;
    }
}
