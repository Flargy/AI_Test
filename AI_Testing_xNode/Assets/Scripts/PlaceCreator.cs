using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceCreator : MonoBehaviour
{
    private BoxCollider col;

    [SerializeField] GameObject Place;
    [SerializeField] int numOfXParts;
    [SerializeField] int numOfZParts;

    private List<BoxCollider> places = new List<BoxCollider>();
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    void Start()
    {
        float colXSize = col.bounds.size.x;
        float colZSize = col.bounds.size.z;

        float XDivide = colXSize / numOfXParts;
        float ZDivide = colZSize / numOfZParts;

        Vector3 pos = col.bounds.min;
        pos.y = col.bounds.center.y;
        pos.x -= XDivide/2;
        pos.z -= ZDivide/2;
        for (int i = 0; i < numOfXParts; i++)
        {
            pos.x += XDivide;
            for (int j = 0; j < numOfZParts; j++)
            {
                pos.z += ZDivide;
                GameObject place = Instantiate(Place, pos, Quaternion.identity);
                BoxCollider placeCol = place.GetComponent<BoxCollider>();
                placeCol.size = new Vector3(XDivide, col.bounds.size.y, ZDivide);
                places.Add(placeCol);

            }

            pos.z = col.bounds.min.z - (ZDivide / 2);
        }
    }

    private void CreateDecisionTree()
    {
        DecisionTree tree = new DecisionTree();
        foreach (BoxCollider place in places)
        {
            DecisionNode.CreateChild(tree.RootNode, place.transform.transform.position, TypeOfObject.PLACE);
        }
        
    }

}
