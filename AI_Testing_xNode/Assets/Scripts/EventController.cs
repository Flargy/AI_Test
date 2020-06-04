using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventController : MonoBehaviour
{
    public static EventController current = null;

    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }

    public event Action LowerProbabilityEvent;
    public void LowerProbability()
    {
        if(LowerProbabilityEvent != null)
        {
            LowerProbabilityEvent();
        }
    }

    public event Action SaveDataEvent;
    public void SaveData()
    {
        if(SaveDataEvent != null)
        {
            SaveDataEvent();
        }
    }

    public event Action ResetHidingEvent;
    public void ResetHiding()
    {
        if(ResetHidingEvent != null)
        {
            ResetHidingEvent();
        }
    }
    
}
