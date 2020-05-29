using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static XNode.Node;

public static class BTDefs 
{
    public const string MOVEMENT_STATE = "MoveState";
}

public enum MovementState
{
    IDLE,
    WALK,
    RUN
}

public enum BTResult
{
    SUCCESS,
    FAILURE,
    RUNNING
}

public enum BehaviourTreeType
{
    AI,
    COUNT
}

public enum PathType
{
    TARGET,
    RANDOM
}

public class BTCompositeAttribute : CreateNodeMenuAttribute
{
    public BTCompositeAttribute(Type _type)
    {
        menuName = "Composites/" + _type.ToString();
    }
}

public class BTAgentAttribute : CreateNodeMenuAttribute
{
    public BTAgentAttribute(Type _type)
    {
        menuName = "Agent/" + _type.ToString();
    }
}