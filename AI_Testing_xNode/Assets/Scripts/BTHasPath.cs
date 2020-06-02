using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BTAgent(typeof(BTHasPath))]
public class BTHasPath : BTNode
{

    public override BTResult Execute()
    {


        if (context.agent.pathPending == false && context.agent.hasPath)
        {

            return BTResult.SUCCESS;
        }
        else
        {
           
            return BTResult.FAILURE;
        }

     
    }

}
