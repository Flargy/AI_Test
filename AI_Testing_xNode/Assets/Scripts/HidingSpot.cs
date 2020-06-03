using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public int probability = 1;
    //public Vector3 position = Vector3.zero;
    public bool visited = false;
    public TypeOfObject type;

    public void UpdateProbability(int newProb)
    {
        probability = newProb;
    }

    
}
