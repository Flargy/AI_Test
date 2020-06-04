using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DecisionNode
{
    public DecisionNode Parent { private set; get; }
    public List<DecisionNode> Children { private set; get; }
    public HidingSpot Spot { private set; get; }
    public TypeOfObject Type { private set; get; }

    /// <summary>
    /// The DecisioNode constructor is only used to create the root node in the tree
    /// </summary>
    /// <param name="parent"> The parent of the new node </param>
    /// <param name="spot"> The HidingSpot that is going to be represented by the DecisionNode</param>
    /// <param name="type"> What type is the new DecisionNode</param>
    public DecisionNode(DecisionNode parent, HidingSpot spot, TypeOfObject type)
    {
        Parent = parent;
        Spot = spot;
        Type = type;
    }

    /// <summary>
    /// Static method to create children of a DecisionNode
    /// </summary>
    /// <param name="parent"> The parent of the new node</param>
    /// <param name="spot"> The HidingSpot that is going to be represented by the DecisionNode</param>
    /// <param name="type">What type is the new DecisionNod</param>
    /// <returns> Returns the new DecisionNode, if the parent is null it will return null</returns>
    public static DecisionNode CreateChild(DecisionNode parent, HidingSpot spot, TypeOfObject type)
    {
        DecisionNode newNode = new DecisionNode(parent, spot, type);

        if(parent != null)
        {
            if (parent.Children == null)
                parent.Children = new List<DecisionNode>();
            parent.Children.Add(newNode);            
            return newNode;

        }
        return null;
    }

    /// <summary>
    /// Get the child DecisionNode that wraps the desired HidingSpot
    /// </summary>
    /// <param name="hidingSpot"> The Hidingspot that is reprecented by the desired DecisionNode </param>
    /// <returns> Reutrns the DecisionNode that wraps the hidingspot, if the hidingSpot is not a child of the DecisionNode it will return null</returns>
    public DecisionNode GetChild(HidingSpot hidingSpot)
    {
        if ( Children != null)
        {
            if (Children.Count != 0)
            {
                foreach (DecisionNode node in Children)
                {

                    if (node.Spot == hidingSpot)
                    {
                        return node;
                    }
                }
            }
        }
        return null;
    }


    /// <summary>
    /// Returns the DecisionNode that contains the HidingSpot that has the highest probability.
    /// If there is mulitple HidingSpots with the same probability ït will return one of the randomly.
    /// </summary>
    /// <returns> The DecisionNode that contains the HidingSpot with the highest probability, or one of the DecisionNode if there are more that one.</returns>
    public DecisionNode GetNodeOfHighestProbability()
    {
        DecisionNode currentBest = null;
        List<DecisionNode> sameNodes = new List<DecisionNode>();

        foreach(DecisionNode node in Children)
        {
            if (node.Children == null || node.Children.Count == 0)
                continue;

            if(currentBest == null || currentBest.Spot.Probability < node.Spot.Probability)
            {
                currentBest = node;
                sameNodes.Clear();
                sameNodes.Add(currentBest);
            }
            else if (currentBest.Spot.Probability ==  node.Spot.Probability)
            {
                sameNodes.Add(node);
            }
        }

        return sameNodes[Random.Range(0, sameNodes.Count)];
    }

    /// <summary>
    /// Returns a DecisionNode randomly based on the HidingSpot's probability.
    /// A HidingSpot with a higher probability has a higher chans of being choosen.
    /// </summary>
    /// <returns> Returns the randomly selected DecisionNode. Returns null if the node do not have any children </returns>
    public DecisionNode GetRandomHidingSpot()
    {
        if (Children == null)
        {
            Parent.Children.Remove(this);
            return null;
        }

        int totalsum = 0;
        foreach (DecisionNode node in Children)
        {

            totalsum += node.Spot.Probability;
        }

        int index = UnityEngine.Random.Range(0, totalsum ) + 1;
        int sum = 0;
        int i = -1;

        while (sum < index)
        {
            sum += Children[i+1].Spot.Probability;
            i++;
        }

        DecisionNode returnNode = Children[Mathf.Max(0, i)];
        return returnNode;
    }

    public override string ToString()
    {
        System.Text.StringBuilder builder = new StringBuilder();
        if (Children != null)
            foreach (DecisionNode child in Children)
            {
                builder.Append(child.ToString());
            }

        return builder.ToString() + $" Position: {Spot}, Type: {Type} ";
    }
}

public enum TypeOfObject
{
    REDCUBE,
    BLUESPHERE,
    YELLOWSYLINDER,
    PLACE,  // Used for the type of the places that contains the HidingSpot's.
    ROOT    // Used only for the root node in the tree.
}
