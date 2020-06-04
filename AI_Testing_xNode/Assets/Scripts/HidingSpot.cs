using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode.Examples.MathNodes;

public class HidingSpot : MonoBehaviour
{
    public int playerProbability = 1;
    public int probability = 1;
    //public Vector3 position = Vector3.zero;
    public bool visited = false;
    public TypeOfObject type;
    public int ID;
    private bool hasBeenUpdated = false;

    public void UpdateProbability(Vector3 playerPostion)
    {
        if (hasBeenUpdated == false)
        {
            hasBeenUpdated = true;
            int value = (int)((playerPostion - this.transform.position).magnitude);

            switch (value)
            {
                case 0:
                    probability = Mathf.Clamp(probability + 3, 1, 10);
                    break;
                case 1:
                    probability = Mathf.Clamp(probability + 2, 1, 10);
                    break;
                default:
                    probability = Mathf.Clamp(probability + 1, 1, 10);
                    break;
            }
        }
    }

    public void UpdateProbability(int addedProbability)
    {
        if (hasBeenUpdated == false)
        {
            hasBeenUpdated = true;
            probability += addedProbability;
        }
    }

    public void Start()
    {
       
        EventController.current.LowerProbabilityEvent += LowerProbability;
        EventController.current.SaveDataEvent += SaveData;
        EventController.current.ResetHidingEvent += Reset;
        LoadData();
    }

    public void Reset()
    {
        hasBeenUpdated = false;
    }

    public void LowerProbability()
    {
        if (hasBeenUpdated == false)
        {
            probability = Mathf.Clamp(probability - 1, 1, 10);
        }
    }

    public void SaveData()
    {
        
        PlayerPrefs.SetInt("HidingSpot" + ID, probability);
        
    }

    public void LoadData()
    {
        
        int temp = PlayerPrefs.GetInt("HidingSpot" + ID);

        if(temp != 0)
        {
            probability = temp;
        }
        
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
