using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    /// <summary>
    /// Subscribes to the even <see cref="EventController.ResetHidingEvent"/> and activates <see cref="SelectHidingSpot"/>
    /// </summary>
    void Start()
    {
        EventController.current.ResetHidingEvent += SelectHidingSpot;
        SelectHidingSpot();
    }

    /// <summary>
    /// Randomizes a hidingspot from <see cref="PlaceCreator.Instance"/> and selects one and changes location to it
    /// </summary>
    private void SelectHidingSpot()
    {
        int totalsum = 0; // The total sum of all probabilities among the hiding spots
        foreach (HidingSpot spot in PlaceCreator.Instance.HidingSpots)
        {

            totalsum += spot.PlayerProbability; // Adds on the probability for each hiding spot
        }

        // Hämtar ett slumpmässing värde från 0 till totalsum-1
        int index = UnityEngine.Random.Range(0, totalsum) + 1; // Randomizes a value between 0 and totalsum
        int sum = 0;
        int i = -1;



        while (sum < index)
        {
            sum += PlaceCreator.Instance.HidingSpots[i + 1].PlayerProbability; // Adds the value of a probability onto sum and stops once sum is equal or greater than the random index value
            i++;
        }


        transform.position = PlaceCreator.Instance.HidingSpots[Mathf.Max(0, i)].transform.position; // Returns the index of the position that was selected
    }
    
   
}
