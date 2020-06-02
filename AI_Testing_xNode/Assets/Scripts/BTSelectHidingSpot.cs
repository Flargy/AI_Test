using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[BTAgent(typeof(BTSelectHidingSpot))]
public class BTSelectHidingSpot : BTNode
{
    int totalsum = 0;
    public override BTResult Execute()
    {
        GetRandom();
    }


    // Hämtar ett gömställe utifrån gömställenas sanorlikhet att bli vald
    public HidingSpot GetRandom()
    {
        foreach (HidingSpot spot in context.contextOwner.internalHidingSpots)
        {
            totalsum += spot.probability;
        }

        // Hämtar ett slumpmässing värde från 0 till totalsum-1
        int index = UnityEngine.Random.Range(0, totalsum);
        int sum = 0;
        int i = 0;

        while(sum < index)
        {
            sum += context.contextOwner.internalHidingSpots[i].probability;
            i++;
        }

        return context.contextOwner.internalHidingSpots[i];

    }
}



//class RandomSelector
//{
//    List<Item> items = new List();
//    Random rand = new Random();
//    int totalSum = 0;

//    RandomSelector()
//    {
//        for (Item item : items)
//        {
//            totalSum = totalSum + item.relativeProb;
//        }
//    }

//    public Item getRandom()
//    {

//        int index = rand.nextInt(totalSum);
//        int sum = 0;
//        int i = 0;
//        while (sum < index)
//        {
//            sum = sum + items.get(i++).relativeProb;
//        }
//        return items.get(Math.max(0, i - 1));
//    }