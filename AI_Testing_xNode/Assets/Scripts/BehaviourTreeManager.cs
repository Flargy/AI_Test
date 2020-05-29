using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BehaviourTreeManager : Singleton<BehaviourTreeManager>
{
    Dictionary<BehaviourTreeType, RuntimeBehaviourTree> behaviourTreeMap;
    Dictionary<BehaviourTreeType, List<BTContextData>> contextDataMap;

    bool behaviourTreeStarting = true;

    public float updateRate = 0.5f;
    public float updateTimer = 0;

    private void Start()
    {
        behaviourTreeMap = BehaviourTreeRuntimeData.GetBehaviourTrees();
        contextDataMap = BehaviourTreeRuntimeData.GetContextData();
    }

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
