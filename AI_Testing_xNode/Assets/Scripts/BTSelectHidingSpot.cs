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
        if (context.agent.pathPending == false && context.agent.hasPath == false)
        {
            if(context.contextOwner.recentNode != null)
            {
                DecisionNode parent = context.contextOwner.recentNode.Parent;
                context.contextOwner.recentNode.Parent.Children.Remove(context.contextOwner.recentNode);
                if(parent.Children.Count == 0)
                {
                    parent.Parent.Children.Remove(parent);
                }
            }

            DecisionNode node = PlaceCreator.Instance.GetNextHidingSpot();
            context.contextOwner.recentNode = node;
            context.contextOwner.destination = node.Spot.transform.position;
            context.agent.SetDestination(node.Spot.transform.position);

            return BTResult.SUCCESS;
        }
        return BTResult.FAILURE;
    }
}