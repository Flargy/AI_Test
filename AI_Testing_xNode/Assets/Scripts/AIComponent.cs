using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIComponent : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public GameObject target;

    internal AIState currentState = AIState.SEARCHING;

    NavMeshAgent navAgent;
    BTContext aiContext;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        aiContext = new BTContext(navAgent, this, transform);
    }

    private void Start()
    {
        BehaviourTreeRuntimeData.RegisterAgentContext(behaviourTreeType, aiContext);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
