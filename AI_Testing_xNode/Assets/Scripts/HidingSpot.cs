using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot
{
    public int probability = 1;
    public Vector3 position = Vector3.zero;
    public bool visited = false;

    public HidingSpot(int prob, Vector3 pos)
    {
        probability = prob;
        position = pos;
    }

    public void UpdateProbability(int newProb)
    {
        probability = newProb;
    }

    
}
