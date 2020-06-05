//Marcus Lundqvist
//Niclas Älmeby

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static XNode.Node;

public static class BTDefs 
{
    public const string MOVEMENT_STATE = "MoveState";
}

/// <summary>
/// Various enum that can be used to set conditions to the agent. Currently not used
/// </summary>
public enum MovementState
{
    IDLE,
    WALK,
    RUN
}

/// <summary>
/// The result values used to determine success of failure in a behaviour tree
/// </summary>
public enum BTResult
{
    SUCCESS,
    FAILURE,
    RUNNING
}

/// <summary>
/// The typed of agent that a behavior tree can control
/// </summary>
public enum BehaviourTreeType
{
    AI,
    COUNT
}

/// <summary>
/// Different path types to determine AI agent behavior
/// </summary>
public enum PathType
{
    TARGET,
    RANDOM
}

/// <summary>
/// Adds a Composite section in <see cref="XNode"/>
/// </summary>
public class BTCompositeAttribute : CreateNodeMenuAttribute
{
    public BTCompositeAttribute(Type _type)
    {
        menuName = "Composites/" + _type.ToString();
    }
}

/// <summary>
/// Adds a Agent section in <see cref="XNode"/>
/// </summary>
public class BTAgentAttribute : CreateNodeMenuAttribute
{
    public BTAgentAttribute(Type _type)
    {
        menuName = "Agent/" + _type.ToString();
    }
}