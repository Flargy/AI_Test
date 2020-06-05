//Marcus Lundqvist
//Niclas Älmeby

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


/// <summary>
/// Fetches a <see cref="DecisionNode"/> from <see cref="PlaceCreator.Instance"/> and sets that as the agents new destination
/// <para>Removes a node once it has been explored</para>
/// </summary>
[BTAgent(typeof(BTSelectHidingSpot))]
public class BTSelectHidingSpot : BTNode
{
    
    public override BTResult Execute()
    {
        if (context.agent.pathPending == false && context.agent.hasPath == false) // CHecks if the agent doesn't have a path
        {
            if(context.contextOwner.recentNode != null)
            {
                DecisionNode parent = context.contextOwner.recentNode.Parent; // Fetches the area from which the last hiding spot was in
                context.contextOwner.recentNode.Parent.Children.Remove(context.contextOwner.recentNode); // Removes the last hiding spot it searched from the area to avoid searching it multiple times
                if(parent.Children.Count == 0)
                {
                    parent.Parent.Children.Remove(parent); // Removes the area as a search spot if no more hiding spots exists in it
                }
            }

            DecisionNode node = PlaceCreator.Instance.GetNextHidingSpot(); // Fetches the next hiding spot from the decision tree
            context.contextOwner.recentNode = node; // Updates the most recent node it searched
            context.contextOwner.destination = node.Spot.transform.position; // Saves the destination of the hiding spot/node
            context.agent.SetDestination(node.Spot.transform.position); // Sets the destination to the node/hiding spot

            return BTResult.SUCCESS;
        }
        return BTResult.FAILURE;
    }
}