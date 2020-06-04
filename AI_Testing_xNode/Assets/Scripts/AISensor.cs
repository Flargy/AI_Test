﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Net.NetworkInformation;

[Serializable]
public class AISensor
{

    public float stopSearchTime;
    public float visionAngle = 60;
    public float visionDistance = 20;
    public float MAX_RADIUS = 2.5f;
    //internal Vector3 lastTargetPosition = Vector3.zero;

    NavMeshAgent agent;
    AIComponent aiComponent;

    float noTargetTimer = 0;

    private bool targetFound = false;


    public void Initialize(AIComponent ai, NavMeshAgent navAgent)
    {
        aiComponent = ai;
        agent = navAgent;
    }

    internal void Update()
    {
        if(aiComponent.target != null)
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        if (IsTargetVisible(aiComponent.target))
        {
            aiComponent.lastKnownPosition = aiComponent.target.transform.position;
            noTargetTimer = 0;
            if (aiComponent.playerFound == false)
            {
                aiComponent.playerFound = true;
            }

            FoundPlayer();
        }
        else
        {
            noTargetTimer += Time.deltaTime;

            if (noTargetTimer >= stopSearchTime)
            {
                agent.ResetPath();
                //aiComponent.target = null;
                noTargetTimer = 0;
                if(aiComponent.playerFound == true)
                {
                    aiComponent.playerFound = false;
                }
            }
        }
    }

    private void FoundPlayer()
    {
        if (targetFound == false)
        {
            targetFound = true;
            Collider[] nearbySpots = Physics.OverlapSphere(aiComponent.target.transform.position, MAX_RADIUS, PlaceCreator.Instance.HidingLayer);
            PlaceCreator.Instance.Tree.GetDecisionNode(nearbySpots[0].GetComponent<HidingSpot>()).Parent.Spot.UpdateProbability(3); // Ökar sannolikheten för platsen

            // Ökar snarolikheten för alla närliggande gömställen
            foreach (Collider item in nearbySpots)
            {
                item.GetComponent<HidingSpot>().UpdateProbability(aiComponent.target.transform.position);
            }

            EventController.current.LowerProbability();
            EventController.current.SaveData();
            EventController.current.ResetHiding();
            PlaceCreator.Instance.CreateDecisionTree();
            agent.Warp(aiComponent.startPosition);
            agent.ResetPath();
            targetFound = false;
        }
    }

    public bool IsTargetVisible(GameObject target)
    {
        bool canSeePlayer = false;

        Vector3 targetPosition = aiComponent.target.transform.position;
        Vector3 selfPosition = aiComponent.transform.position;

        selfPosition.y += 1;
        targetPosition.y += 1;

        Vector3 directionToPlayer = (targetPosition - selfPosition);

        if(directionToPlayer.sqrMagnitude < visionDistance * visionDistance)
        {
            Vector3 targetDirection = directionToPlayer.normalized;
            targetDirection.y = 0;

            if(Vector3.Angle(aiComponent.transform.forward, targetDirection) < (visionAngle / 2))
            {
                RaycastHit hit;
                if(Physics.Raycast(selfPosition, targetDirection, out hit, visionDistance, LayerMask.GetMask("Water")))
                {
                  
                    canSeePlayer = hit.transform.gameObject == aiComponent.target;
                    

                }
            }
        }

        return canSeePlayer;
    }

}
