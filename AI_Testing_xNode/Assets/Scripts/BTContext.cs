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
    public GameObject player;

    public BTContext(NavMeshAgent navAgent, AIComponent owner, Transform ownerTransform, GameObject target)
    {
        agent = navAgent;
        contextOwner = owner;
        transform = ownerTransform;
        player = target;
    }
}
