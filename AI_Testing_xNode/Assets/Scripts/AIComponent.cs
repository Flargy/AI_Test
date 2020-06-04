using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIComponent : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public GameObject target;
    public bool playerFound = false;
    public DecisionNode recentNode = null;

    internal AIState currentState = AIState.SEARCHING;

    NavMeshAgent navAgent;
    BTContext aiContext;
    public AISensor sensor;

    public Vector3[] patrolPoints;

    [HideInInspector] public Vector3 alertLocation;
    [HideInInspector] public bool alerted = false;
    [HideInInspector] public Vector3 lastKnownPosition = Vector3.zero;
    [HideInInspector] public Vector3 destination = Vector3.zero;
    [HideInInspector] public Vector3 startPosition = Vector3.zero;

    public void Reset()
    {
        destination = Vector3.zero;
        playerFound = false;
        recentNode = null;
        alerted = false;
        lastKnownPosition = Vector3.zero;
        alertLocation = Vector3.zero;
    }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        aiContext = new BTContext(navAgent, this, transform, target);
    }

    private void Start()
    {
        startPosition = transform.position;
        sensor.Initialize(this, navAgent);
        BehaviourTreeRuntimeData.RegisterAgentContext(behaviourTreeType, aiContext);
        Time.timeScale = 2;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Update()
    {
        sensor.Update();
        
    }

    public void Alert(Vector3 location)
    {
        alertLocation = location;
        alerted = false;
    }

    

}
