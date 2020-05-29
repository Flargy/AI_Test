using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BTContext
{
    public NavMeshAgent navAgent;
    public AIComponent contextOwner;
    public List<string> behaviourHistory = new List<string>();

    public BTContext(NavMeshAgent _navAgent, AIComponent _owner)
    {
        navAgent = _navAgent;
        contextOwner = _owner;
    }
}
