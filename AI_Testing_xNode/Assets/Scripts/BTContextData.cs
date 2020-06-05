//Marcus Lundqvist
//Niclas Älmeby

using System.Collections.Generic;

public class BTContextData
{
    /// <summary>
    /// Sets the <see cref="BTContext"/> for the behavior tree
    /// </summary>
    /// <param name="context"></param>
    public BTContextData(BTContext context)
    {
        owningContext = context;
    }

    public BTContext owningContext;
    List<BTNode> runningNodes = new List<BTNode>();

    /// <summary>
    /// Adds a node to <see cref="runningNodes"/>
    /// </summary>
    /// <param name="_node"></param>
    public void AddRunningNode(BTNode _node)
    {
        if (!runningNodes.Contains(_node))
        {
            runningNodes.Insert(0, _node);
        }
    }

    /// <summary>
    /// Removes node from <see cref="runningNodes"/>
    /// </summary>
    /// <param name="nodeToRemove"></param>
    public void RemoveRunningNode(BTNode nodeToRemove)
    {
        runningNodes.Remove(nodeToRemove);
    }

    /// <summary>
    /// Checks if <see cref="runningNodes"/> contains <see cref="BTNode"/> runningNode
    /// </summary>
    /// <param name="runningNode"></param>
    /// <returns></returns>
    public bool HasRunningNodes(out BTNode runningNode)
    {
        runningNode = null;

        if (runningNodes.Count != 0)
        {
            runningNode = runningNodes[0];
            return true;
        }
        else return false;
    }
}
