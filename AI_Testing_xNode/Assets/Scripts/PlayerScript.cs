using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{


    void Start()
    {
        EventController.current.ResetHidingEvent += SelectHidingSpot;
        SelectHidingSpot();
    }

    private void SelectHidingSpot()
    {
        int totalsum = 0;
        foreach (HidingSpot spot in PlaceCreator.Instance.HidingSpots)
        {

            totalsum += spot.playerProbability;
        }

        // Hämtar ett slumpmässing värde från 0 till totalsum-1
        int index = UnityEngine.Random.Range(0, totalsum) + 1;
        int sum = 0;
        int i = -1;



        while (sum < index)
        {
            sum += PlaceCreator.Instance.HidingSpots[i + 1].playerProbability;
            i++;
        }


        transform.position = PlaceCreator.Instance.HidingSpots[Mathf.Max(0, i)].transform.position;
    }
    
   
}
