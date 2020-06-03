using System.Collections.Generic;
using UnityEngine;

public class DecisionNode
{
    public DecisionNode Parent { private set; get; }
    public List<DecisionNode> Children { private set; get; }

    public Vector3 Position { private set; get; }

    public TypeOfObject Type { private set; get; }

    private DecisionNode(DecisionNode parent, Vector3 position, TypeOfObject type)
    {
        Parent = parent;
        Position = position;
        Type = type;
    }

    public static DecisionNode CreateChild(DecisionNode parent, Vector3 pos, TypeOfObject type)
    {
        DecisionNode newNode = new DecisionNode(parent, pos, type);
        if(parent != null)
            parent.Children.Add(newNode);

        return newNode;
    }

    public override string ToString()
    {
        return $"Position: {Position}, Type: {Type}"; 
    }
}


public enum TypeOfObject
{
    REDCUBE,
    BLUESPHERE,
    YELLOWCONE,
    PLACE,  // Används för en generell plats
    ROOT    // Används för att ge roten ett typvärde
}
