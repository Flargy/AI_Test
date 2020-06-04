using UnityEngine;

[BTAgent(typeof(BTIsAlerted))]
public class BTIsAlerted : BTNode
{
    [Input] public BTResult inResult; // The indata for the node in Xnode

    /// <summary>
    /// Returns whether the agent has been alerted by the player's presence
    /// <para>OBS: Discarded function that isn't complete</para>
    /// </summary>
    /// <returns></returns>
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
