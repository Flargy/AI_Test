using System.Collections.Generic;

public static class BehaviourTreeRuntimeData
{
    static Dictionary<BehaviourTreeType, RuntimeBehaviourTree> behaviourTreeMap = new Dictionary<BehaviourTreeType, RuntimeBehaviourTree>();
    static Dictionary<BehaviourTreeType, List<BTContextData>> contextMap = new Dictionary<BehaviourTreeType, List<BTContextData>>();



    /// <summary>
    /// Registers behavior tree that will be used during runtime
    /// </summary>
    /// <param name="runtimeTree"></param>
    public static void RegisterBehaviourTree(RuntimeBehaviourTree runtimeTree)
    {
        behaviourTreeMap[runtimeTree.behaviourTreeType] = runtimeTree;
    }


    /// <summary>
    /// Registers the <see cref="BTContextData"/> for the behavior tree
    /// </summary>
    /// <param name="behaviourType"></param>
    /// <param name="aiContext"></param>
    public static void RegisterAgentContext(BehaviourTreeType behaviourType, BTContext aiContext)
    {
        if (!contextMap.ContainsKey(behaviourType))
        {
            contextMap[behaviourType] = new List<BTContextData>();
        }

        contextMap[behaviourType].Add(new BTContextData(aiContext));
    }


    /// <summary>
    /// Removes a registered <see cref="BTContext"/> from <see cref="contextMap"/>
    /// </summary>
    /// <param name="behaviourType"></param>
    /// <param name="aiContext"></param>
    public static void UnregisterAgentContext(BehaviourTreeType behaviourType, BTContext aiContext)
    {
        if (contextMap.ContainsKey(behaviourType))
        {
            BTContextData data = contextMap[behaviourType].Find(x => x.owningContext == aiContext);
            if (data != null)
            {
                contextMap[behaviourType].Remove(data);
            }
        }
    }


    /// <summary>
    /// Adds node that is currently running and active
    /// </summary>
    /// <param name="nodeContext"></param>
    /// <param name="node"></param>
    public static void AddRunningNode(BTContext nodeContext, BTNode node)
    {
        BTContextData data = contextMap[nodeContext.contextOwner.behaviourTreeType].Find(x => x.owningContext == nodeContext);
        if (data != null)
        {
            data.AddRunningNode(node);
        }
    }


    /// <summary>
    /// Removes a currently running node
    /// </summary>
    /// <param name="nodeContext"></param>
    /// <param name="node"></param>
    public static void RemoveRunningNode(BTContext nodeContext, BTNode node)
    {
        BTContextData data = contextMap[nodeContext.contextOwner.behaviourTreeType].Find(x => x.owningContext == nodeContext);
        if (data != null)
        {
            data.RemoveRunningNode(node);
        }
    }

    /// <summary>
    /// Returns <see cref="behaviourTreeMap"/>
    /// </summary>
    /// <returns></returns>
    public static Dictionary<BehaviourTreeType, RuntimeBehaviourTree> GetBehaviourTrees()
    {
        return behaviourTreeMap;
    }

    /// <summary>
    /// Returns <see cref="contextMap"/>
    /// </summary>
    /// <returns></returns>
    public static Dictionary<BehaviourTreeType, List<BTContextData>> GetContextData()
    {
        return contextMap;
    }
}
