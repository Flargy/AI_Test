using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree
{
    public DecisionNode RootNode { get; private set; }

    public DecisionTree()
    {
        RootNode = DecisionNode.CreateChild(null, Vector3.zero, TypeOfObject.ROOT);
    }


    //public string ToString()
    //{

    //}
}
