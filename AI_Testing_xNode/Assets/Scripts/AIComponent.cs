﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIComponent : MonoBehaviour
{
    public BehaviourTreeType behaviourTreeType;
    public GameObject target;
    public bool playerFound = false;

    internal AIState currentState = AIState.SEARCHING;

    NavMeshAgent navAgent;
    BTContext aiContext;
    public AISensor sensor;

    public Transform[] hidingSpots;
    public Vector3[] patrolPoints;

    [HideInInspector] public Vector3 alertLocation;
    [HideInInspector] public bool alerted = false;
    [HideInInspector] public Vector3 lastKnownPosition = Vector3.zero;
    [HideInInspector] public HidingSpot[] internalHidingSpots;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        aiContext = new BTContext(navAgent, this, transform, target);
        internalHidingSpots = new HidingSpot[hidingSpots.Length];
    }

    private void Start()
    {
        sensor.Initialize(this, navAgent);
        BehaviourTreeRuntimeData.RegisterAgentContext(behaviourTreeType, aiContext);

        for(int i = 0; i < hidingSpots.Length; i++)
        {
            internalHidingSpots[i] = new HidingSpot(1, hidingSpots[i].position);
        }
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
