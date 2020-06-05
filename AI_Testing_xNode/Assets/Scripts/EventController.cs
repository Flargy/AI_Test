//Marcus Lundqvist
//Niclas Älmeby

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventController : MonoBehaviour
{
    public static EventController current = null; // Variable for singletin instance

    /// <summary>
    /// Creates a singleton instance of this script
    /// </summary>
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }

    /// <summary>
    /// Event that scripts can subscribe to
    /// </summary>
    public event Action LowerProbabilityEvent;
    public void LowerProbability()
    {
        if(LowerProbabilityEvent != null)
        {
            LowerProbabilityEvent();
        }
    }

    /// <summary>
    /// Event that scripts can subscribe to
    /// </summary>
    public event Action SaveDataEvent;
    public void SaveData()
    {
        if(SaveDataEvent != null)
        {
            SaveDataEvent();
        }
    }

    /// <summary>
    /// Event that scripts can subscribe to
    /// </summary>
    public event Action ResetHidingEvent;
    public void ResetHiding()
    {
        if(ResetHidingEvent != null)
        {
            ResetHidingEvent();
        }
    }
    
}
