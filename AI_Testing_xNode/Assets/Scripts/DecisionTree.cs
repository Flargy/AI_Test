//Marcus Lundqvist
//Niclas Älmeby

using System.Text;
using UnityEngine;

public class DecisionTree
{
    public DecisionNode RootNode { get; private set; }

    public DecisionTree()
    {
        RootNode = new DecisionNode(null, null, TypeOfObject.ROOT);
    }

    /// <summary>
    /// Get the DecisionNode that contains the hidingSpot, if the HidingSpot don't exist in the tree it will reutnr null.
    /// </summary>
    /// <param name="hidingSpot"> The HidingSpot that is containd in the returning DecisionNode</param>
    /// <returns> The DecisionNode that contains hidingSpot if one exists, otherwise returns null </returns>
    public DecisionNode GetDecisionNode(HidingSpot hidingSpot)
    {
        foreach (DecisionNode node in PlaceCreator.Instance.Tree.RootNode.Children)
        {

            DecisionNode Cnode = node.GetChild(hidingSpot);
            if (Cnode != null)
            {

                return Cnode;
            }
        }

        return null;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (RootNode == null)
            return "root node is null";
        foreach (DecisionNode child in RootNode.Children)
        {
            builder.Append(child.ToString());
        }

        return builder.ToString();
    }
}
