﻿using System.Text;
using UnityEngine;

public class DecisionTree
{
    public DecisionNode RootNode { get; private set; }

    public DecisionTree()
    {
        RootNode = new DecisionNode(null, null, TypeOfObject.ROOT);
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
