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


    /// <summary>
    /// Constructor for <see cref="BTContext"/>
    /// </summary>
    /// <param name="navAgent">The <see cref="NavMeshAgent"/> on the agent</param>
    /// <param name="owner">The <see cref="AIComponent"/> script attached to the agent</param>
    /// <param name="ownerTransform">The owner's <see cref="Transform"/></param>
    /// <param name="target">The target of the agent</param>
    public BTContext(NavMeshAgent navAgent, AIComponent owner, Transform ownerTransform, GameObject target)
    {
        agent = navAgent;
        contextOwner = owner;
        transform = ownerTransform;
        player = target;
    }
}
