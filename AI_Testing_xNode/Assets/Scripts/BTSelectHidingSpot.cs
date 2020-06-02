using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BTAgent(typeof(BTSelectHidingSpot))]
public class BTSelectHidingSpot : BTNode
{
    int totalsum = 0;
    public override BTResult Execute()
    {
        


        foreach(HidingSpot spot in context.contextOwner.internalHidingSpots)
        {
            totalsum += spot.probability;
        }


        throw new System.NotImplementedException();
    }

    public HidingSpot GetRandom()
    {

        int index = Random.Range(0, totalsum);
        
    }
}



class RandomSelector
{
    List<Item> items = new List();
    Random rand = new Random();
    int totalSum = 0;

    RandomSelector()
    {
        for (Item item : items)
        {
            totalSum = totalSum + item.relativeProb;
        }
    }

    public Item getRandom()
    {

        int index = rand.nextInt(totalSum);
        int sum = 0;
        int i = 0;
        while (sum < index)
        {
            sum = sum + items.get(i++).relativeProb;
        }
        return items.get(Math.max(0, i - 1));
    }