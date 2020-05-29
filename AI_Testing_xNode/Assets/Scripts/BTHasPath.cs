using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BTAgent(typeof(BTHasPath))]
public class BTHasPath : BTNode
{

    public override BTResult Execute()
    {

        BTResult result = BTResult.FAILURE;

        if (context.navAgent.hasPath)
        {
            result = BTResult.SUCCESS;
        }
        else
        {
            result = BTResult.FAILURE;
        }

        return result;
    }

}
