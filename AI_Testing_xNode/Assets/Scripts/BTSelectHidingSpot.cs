// Marcus Lundqvist
// Niclas Älmeby

using System.Diagnostics;
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
            // Prunes the recentNode and sents a new one. Also prunes the parent of recentNode if it has no children.
            if(context.contextOwner.RecentNode != null)
            {
                context.contextOwner.ParentNode = context.contextOwner.RecentNode.Parent;
                context.contextOwner.RecentNode.Spot.DisableUI();
                context.contextOwner.RecentNode.Parent.Children.Remove(context.contextOwner.RecentNode);

                if(context.contextOwner.ParentNode.Children.Count == 0)
                {
                    context.contextOwner.ParentNode.Spot.DisableUI();
                    context.contextOwner.ParentNode.Parent.Children.Remove(context.contextOwner.ParentNode);
                    context.contextOwner.ParentNode = null;
                }
            }

            // Checks if the parentNode has not yet been fully explored, and if it has not been choose the next hiding spot from the parent.
            // Otherwise select new hiding spot from the whole decision tree.
            if(context.contextOwner.ParentNode != null)
            {
                DecisionNode node = context.contextOwner.ParentNode.GetRandomHidingSpot();
                context.contextOwner.RecentNode = node;
                context.contextOwner.destination = node.Spot.transform.position;
                context.agent.SetDestination(node.Spot.transform.position);
            }
            else
            {
                DecisionNode node = PlaceCreator.Instance.GetNextHidingSpot();
                context.contextOwner.RecentNode = node;
                context.contextOwner.destination = node.Spot.transform.position;
                context.agent.SetDestination(node.Spot.transform.position);
                context.contextOwner.ParentNode = node.Parent;
            }
           

            return BTResult.SUCCESS;
        }
        return BTResult.FAILURE;
    }
}