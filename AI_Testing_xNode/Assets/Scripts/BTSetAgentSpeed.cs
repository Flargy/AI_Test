using UnityEngine;
[BTAgent(typeof(BTSetAgentSpeed))]
public class BTSetAgentSpeed : BTNode
{
    public float desiredSpeed;
    public override BTResult Execute()
    {
        context.agent.speed = desiredSpeed;
        return BTResult.SUCCESS;
    }
}
