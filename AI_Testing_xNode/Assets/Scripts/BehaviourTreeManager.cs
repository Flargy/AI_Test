//Marcus Lundqvist
//Niclas Älmeby

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BehaviourTreeManager : Singleton<BehaviourTreeManager>
{
    Dictionary<BehaviourTreeType, RuntimeBehaviourTree> behaviourTreeMap;
    Dictionary<BehaviourTreeType, List<BTContextData>> contextDataMap;

    bool behaviourTreeStarting = true; // Bool to make sure that the behavior trees are only initialized once

    public float updateRate = 0.5f; // Controls the update rate of the behavior trees
    public float updateTimer = 0;

    private void Start()
    {
        behaviourTreeMap = BehaviourTreeRuntimeData.GetBehaviourTrees(); // Retreives the static dictionary from BehaviourTreeRuntimeData
        contextDataMap = BehaviourTreeRuntimeData.GetContextData(); // Retreives the static dictionary from BehaviourTreeRuntimeData
    }


    /// <summary>
    /// Updates the behaviour trees on a set interval by clearing their history and starting them again
    /// </summary>
    private void Update() 
    {
        if(behaviourTreeStarting == true)
        {
            behaviourTreeStarting = false;
            InitializeAllBehaviourTrees();
        }

        if (updateTimer > updateRate)
        {
#if UNITY_EDITOR
            ClearAgentHistory();
#endif
            updateTimer = 0;
            StartAgents();
        }
        else updateTimer += Time.deltaTime;
    }


    /// <summary>
    /// Activates every behavior tree in <see cref="BehaviourTreeType"/>
    /// </summary>
    private void StartAgents()
    {
        for(int i = 0; i < (int)BehaviourTreeType.COUNT; i++)
        {
            BehaviourTreeType treeType = (BehaviourTreeType)i;

            if (behaviourTreeMap.ContainsKey(treeType))
            {
                if (contextDataMap.ContainsKey(treeType))
                {
                    contextDataMap[treeType].ForEach(x => behaviourTreeMap[treeType].RunBehaviourTree(x));
                }
            }
        }
    }

    /// <summary>
    /// Clears the history of every behavior tree in <see cref="BehaviourTreeType"/>
    /// </summary>
#if UNITY_EDITOR
    private void ClearAgentHistory()
    {
        for (int i = 0; i < (int)BehaviourTreeType.COUNT; ++i)
        {
            BehaviourTreeType treeType = (BehaviourTreeType)i;

            if (behaviourTreeMap.ContainsKey(treeType))
            {
                if (contextDataMap.ContainsKey(treeType))
                {
                    contextDataMap[treeType].ForEach(x => x.owningContext.behaviourHistory.Clear());
                }
            }
        }
    }
#endif

    /// <summary>
    /// Initiates the first startup of every behavior tree in <see cref="BehaviourTreeType"/>
    /// </summary>
    private void InitializeAllBehaviourTrees()
    {
        for (int i = 0; i < (int)BehaviourTreeType.COUNT; ++i)
        {
            BehaviourTreeType treeType = (BehaviourTreeType)i;

            if (behaviourTreeMap.ContainsKey(treeType))
            {
                InitializeTreeNodes(behaviourTreeMap[treeType].runtimeTree.nodes);
            }
        }
    }

    /// <summary>
    /// Initializes the nodes in the tree for usage
    /// </summary>
    /// <param name="nodeList"></param>
    private void InitializeTreeNodes(List<Node> nodeList)
    {
        foreach (Node node in nodeList)
        {
            BTNode btNode = (BTNode)node;
            if (btNode != null)
            {
                if (btNode is BTSubTree)
                {
                    btNode.OnStart();
                    InitializeTreeNodes(((BTSubTree)btNode).subTree.nodes);
                }
                else btNode.OnStart();
            }
        }
    }


    /// <summary>
    /// Returns <see cref="BTContextData"/> for each behavior tree in <see cref="contextDataMap"/>
    /// </summary>
    /// <returns></returns>
    public List<BTContextData> GetAllContextData()
    {
        List<BTContextData> dataList = new List<BTContextData>();

        foreach (KeyValuePair<BehaviourTreeType, List<BTContextData>> keyPair in contextDataMap)
        {
            keyPair.Value.ForEach(x => dataList.Add(x));
        }

        return dataList;
    }
}
