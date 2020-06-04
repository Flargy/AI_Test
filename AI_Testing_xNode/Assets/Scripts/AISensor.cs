using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Net.NetworkInformation;

[Serializable]
public class AISensor
{

    public float stopSearchTime; // The amount of time the agent will search for the player after losing them in a chase
    public float visionAngle = 60; // The vision angle forward of the agent
    public float visionDistance = 20; // The distance of which the agent can see
    const float MAX_RADIUS = 2.5f; // The max radius of which it stores data around after finding the player
    //internal Vector3 lastTargetPosition = Vector3.zero;

    NavMeshAgent agent; // Reference to the NavMeshAgent
    AIComponent aiComponent; // Reference to the AIComponent

    float noTargetTimer = 0; // A variable that will keep track of how long the agent has been without a target after losing them in a chase

    private bool targetFound = false; // bool to see if the agent has found the target or not

    /// <summary>
    /// Registers the values of <see cref="aiComponent"/> and <see cref="agent"/>
    /// </summary>
    /// <param name="ai"></param>
    /// <param name="navAgent"></param>
    public void Initialize(AIComponent ai, NavMeshAgent navAgent)
    {
        aiComponent = ai;
        agent = navAgent;
    }

    /// <summary>
    /// Tries to see the target if one exists
    /// </summary>
    internal void Update()
    {
        if(aiComponent.target != null)
        {
            FindTarget();
        }
    }

    /// <summary>
    /// Updates how long the agent has been without a target if it was lost in a chase. Saves the last known position as long as it can see the player.
    /// </summary>
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

    /// <summary>
    /// Activates events for resetting the level when the agent finds the player. Saves all hiding positions near the player when found and increases the probability of searching those spots in future tries. 
    /// Increases probability of searching in the zone where the player was last found.
    /// </summary>
    private void FoundPlayer()
    {
        if (targetFound == false)
        {
            targetFound = true;
            Collider[] nearbySpots = Physics.OverlapSphere(aiComponent.target.transform.position, MAX_RADIUS, PlaceCreator.Instance.HidingLayer);
            PlaceCreator.Instance.Tree.GetDecisionNode(nearbySpots[0].GetComponent<HidingSpot>()).Parent.Spot.UpdateProbability(3); // Increases the probability that the agent will search the zone in the next try.

            // Increases the probability that the agent searches the hiding spots near where the player was found.
            foreach (Collider item in nearbySpots)
            {
                item.GetComponent<HidingSpot>().UpdateProbability(aiComponent.target.transform.position);
            }

            EventController.current.LowerProbability(); // Lowers the probability of all other
            EventController.current.SaveData(); // Saves the probability of each zone and hiding spot
            EventController.current.ResetHiding(); // Resets the player position
            PlaceCreator.Instance.CreateDecisionTree(); // Creates a new decision tree
            // The following 3 lines resets the agent's position, path and variable controlling if it has found the player
            agent.Warp(aiComponent.startPosition);
            agent.ResetPath();
            targetFound = false;
        }
    }

    /// <summary>
    /// Checks if the vision to the player isn't obscured
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsTargetVisible(GameObject target)
    {
        bool canSeePlayer = false;

        Vector3 targetPosition = aiComponent.target.transform.position;
        Vector3 selfPosition = aiComponent.transform.position;

        selfPosition.y += 1;
        targetPosition.y += 1;

        Vector3 directionToPlayer = (targetPosition - selfPosition);

        if(directionToPlayer.sqrMagnitude < visionDistance * visionDistance) // Chekcs if the player is within range to be seen
        {
            Vector3 targetDirection = directionToPlayer.normalized;
            targetDirection.y = 0;

            if(Vector3.Angle(aiComponent.transform.forward, targetDirection) < (visionAngle / 2)) // Checks if the angle of the players position is within the agent's field of view
            {
                RaycastHit hit;
                if(Physics.Raycast(selfPosition, targetDirection, out hit, visionDistance, LayerMask.GetMask("Water"))) // Sends a raycast towards the player to see if it hits
                {
                  
                    canSeePlayer = hit.transform.gameObject == aiComponent.target; // Checks if the hit object is the same as the current target
                    

                }
            }
        }

        return canSeePlayer;
    }

}
