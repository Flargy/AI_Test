using System.Collections.Generic;

public static class BehaviourTreeRuntimeData
{
    static Dictionary<BehaviourTreeType, RuntimeBehaviourTree> behaviourTreeMap = new Dictionary<BehaviourTreeType, RuntimeBehaviourTree>();
    static Dictionary<BehaviourTreeType, List<BTContextData>> contextMap = new Dictionary<BehaviourTreeType, List<BTContextData>>();


    public static void RegisterBehaviourTree(RuntimeBehaviourTree runtimeTree)
    {
        behaviourTreeMap[runtimeTree.behaviourTreeType] = runtimeTree;
    }

    public static void RegisterAgentContext(BehaviourTreeType behaviourType, BTContext aiContext)
    {
        if (!contextMap.ContainsKey(behaviourType))
        {
            contextMap[behaviourType] = new List<BTContextData>();
        }

        contextMap[behaviourType].Add(new BTContextData(aiContext));
    }

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

    public static void AddRunningNode(BTContext nodeContext, BTNode node)
    {
        BTContextData data = contextMap[nodeContext.contextOwner.behaviourTreeType].Find(x => x.owningContext == nodeContext);
        if (data != null)
        {
            data.AddRunningNode(node);
        }
    }

    public static void RemoveRunningNode(BTContext nodeContext, BTNode node)
    {
        BTContextData data = contextMap[nodeContext.contextOwner.behaviourTreeType].Find(x => x.owningContext == nodeContext);
        if (data != null)
        {
            data.RemoveRunningNode(node);
        }
    }

    public static Dictionary<BehaviourTreeType, RuntimeBehaviourTree> GetBehaviourTrees()
    {
        return behaviourTreeMap;
    }

    public static Dictionary<BehaviourTreeType, List<BTContextData>> GetContextData()
    {
        return contextMap;
    }
}
