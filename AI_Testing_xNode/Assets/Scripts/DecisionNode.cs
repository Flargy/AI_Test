using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DecisionNode
{
    public DecisionNode Parent { private set; get; }
    public List<DecisionNode> Children { private set; get; }

    public HidingSpot Spot { private set; get; }

    public TypeOfObject Type { private set; get; }

    public DecisionNode(DecisionNode parent, HidingSpot spot, TypeOfObject type)
    {
        Parent = parent;
        Spot = spot;
        Type = type;
    }

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

    public DecisionNode GetChild(HidingSpot hidingSpot)
    {
        foreach(DecisionNode node in Children)
        {
            if (node.Spot == hidingSpot)
            {
                return node;
            }
        }
        return null;
    }

    public override string ToString()
    {
        System.Text.StringBuilder builder = new StringBuilder();
        if(Children != null)
            foreach (DecisionNode child in Children)
            {
                builder.Append(child.ToString());
            }

        return builder.ToString() + $" Position: {Spot}, Type: {Type} "; 
    }

    public DecisionNode GetNodeOfHighestProbability()
    {
        DecisionNode currentBest = null;
        List<DecisionNode> sameNodes = new List<DecisionNode>();

        foreach(DecisionNode node in Children)
        {
            if (node.Children == null || node.Children.Count == 0)
                continue;

            if(currentBest == null || currentBest.Spot.probability < node.Spot.probability)
            {
                currentBest = node;
                sameNodes.Clear();
                sameNodes.Add(currentBest);
            }
            else if (currentBest.Spot.probability ==  node.Spot.probability)
            {
                sameNodes.Add(node);
            }
        }

        return sameNodes[Random.Range(0, sameNodes.Count)];
    }

    public HidingSpot GetRandomHidingSpot()
    {
        if (Children == null)
        {
            Parent.Children.Remove(this);
            return null;
        }

        int totalsum = 0;
        foreach (DecisionNode node in Children)
        {

            totalsum += node.Spot.probability;
        }

        // Hämtar ett slumpmässing värde från 0 till totalsum-1
        int index = UnityEngine.Random.Range(0, totalsum ) + 1;
        int sum = 0;
        int i = -1;
        Debug.Log("total sum " + totalsum + " index: " + index);
        while (sum < index)
        {
            sum += Children[i+1].Spot.probability;
            i++;
        }
        Debug.Log("i " + i);

        DecisionNode returnNode = Children[Mathf.Max(0, i)];
        Children.Remove(returnNode);
        if (Children.Count == 0)
            Parent.Children.Remove(this);

        Debug.Log("returing spot " + returnNode.Spot);
        return returnNode.Spot;

    }
}


public enum TypeOfObject
{
    REDCUBE,
    BLUESPHERE,
    YELLOWSYLINDER,
    PLACE,  // Används för en generell plats
    ROOT    // Används för att ge roten ett typvärde
}
