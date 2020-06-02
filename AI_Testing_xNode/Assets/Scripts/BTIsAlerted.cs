using UnityEngine;

[BTAgent(typeof(BTIsAlerted))]
public class BTIsAlerted : BTNode
{
    [Input] public BTResult inResult;

    public override BTResult Execute()
    {
        if(context.contextOwner.alerted == false)
        {
            return BTResult.FAILURE;
        }
        else
        {
            return inResult;
        }
    }
}
