using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BTContext
{
    public NavMeshAgent agent;
    public AIComponent contextOwner;
    public Transform transform;
    public List<string> behaviourHistory = new List<string>();

    public BTContext(NavMeshAgent navAgent, AIComponent owner, Transform ownerTransform)
    {
        agent = navAgent;
        contextOwner = owner;
        transform = ownerTransform;
    }
}
