using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class AISensor
{

    public float stopSearchTime;
    public float visionAngle = 60;
    public float visionDistance = 20;

    //internal Vector3 lastTargetPosition = Vector3.zero;

    NavMeshAgent agent;
    AIComponent aiComponent;

    float noTargetTimer = 0;


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
