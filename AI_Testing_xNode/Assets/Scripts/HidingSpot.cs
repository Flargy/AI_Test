using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public int probability = 1;
    //public Vector3 position = Vector3.zero;
    public bool visited = false;
    public TypeOfObject type;
    public int ID;

    public void UpdateProbability(int newProb)
    {
        probability = newProb;
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
        
        UpdateProbability(PlayerPrefs.GetInt("HidingSpot" + ID));
        
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
