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
}


public enum TypeOfObject
{
    REDCUBE,
    BLUESPHERE,
    YELLOWSYLINDER,
    PLACE,  // Används för en generell plats
    ROOT    // Används för att ge roten ett typvärde
}
