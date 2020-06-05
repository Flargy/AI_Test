//Marcus Lundqvist
//Niclas Älmeby

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The class containing all information that the AI agent will have available during runtime
/// </summary>
public class AIComponent : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType; // The type of behavior tree this agent will be seen as
    public GameObject target; // The target of the agent, in this case the player
    public bool playerFound = false; // a boolean to keep track of whether it has found the player or not
    public DecisionNode recentNode = null; // The current decision node representing the area to investigate


    NavMeshAgent navAgent; // The navmeshagent connected to the object
    BTContext aiContext; // The BTContext sent into the behavior tree to identify the agent
    public AISensor sensor; // The sensor controlling how the AI can see the player

    public Vector3[] patrolPoints;

    [HideInInspector] public Vector3 alertLocation; // Not currently used
    [HideInInspector] public bool alerted = false; // Not currently used
    [HideInInspector] public Vector3 lastKnownPosition = Vector3.zero; // The last known position of the target, used if the agent can chase the player
    [HideInInspector] public Vector3 destination = Vector3.zero; // The current destination of the navmeshagent
    [HideInInspector] public Vector3 startPosition = Vector3.zero; // The start position of the agent

    
    /// <summary>
    /// Sets the values of <see cref="navAgent"/> and <see cref="aiContext"/>
    /// </summary>
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        aiContext = new BTContext(navAgent, this, transform, target);
    }

    /// <summary>
    /// Saves the starting position value. Initializes the <see cref="AISensor"/> and registers the agent in <see cref="BehaviourTreeRuntimeData"/>
    /// </summary>
    private void Start()
    {
        startPosition = transform.position;
        sensor.Initialize(this, navAgent);
        BehaviourTreeRuntimeData.RegisterAgentContext(behaviourTreeType, aiContext);
    }

    /// <summary>
    /// Returns the position of the agent
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Updates the sensory system of the agent
    /// </summary>
    public void Update()
    {
        sensor.Update();
        
    }

    /// <summary>
    /// Alerts the agent of a location
    /// <para>OBS: currently ont used</para>
    /// </summary>
    /// <param name="location"></param>
    public void Alert(Vector3 location)
    {
        alertLocation = location;
        alerted = false;
    }

    

}
