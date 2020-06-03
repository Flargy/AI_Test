using System.Text;
using UnityEngine;

public class DecisionTree
{
    public DecisionNode RootNode { get; private set; }

    public DecisionTree()
    {
        RootNode = DecisionNode.CreateChild(null, null, TypeOfObject.ROOT);
    }


    public override string ToString()
    {
        //StringBuilder builder = new StringBuilder();
        //if (RootNode == null)
        //    return "";
        //foreach(DecisionNode child in RootNode.Children)
        //{
        //    builder.Append(child.ToString());
        //}

        //return builder.ToString();

        return "bajs";
    }
}
