using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.Examples.MathNodes;

public class HidingSpot : MonoBehaviour
{
    public int probability = 1;
    //public Vector3 position = Vector3.zero;
    public bool visited = false;
    public TypeOfObject type;
    public int ID;

    public void UpdateProbability(Vector3 playerPostion)
    {
        probability += (int)((playerPostion - this.transform.position).magnitude * 0.5f);
    }

    public void UpdateProbability(int addedProbability)
    {
        probability += addedProbability;
    }

    public void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        
        PlayerPrefs.SetInt("HidingSpot" + ID, probability);
        
    }

    public void LoadData()
    {
        probability = PlayerPrefs.GetInt("HidingSpot" + ID);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

}
