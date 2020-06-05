using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceCreator : MonoBehaviour
{
    public static PlaceCreator Instance = null;
    public DecisionTree Tree { private set; get; }
    public LayerMask HidingLayer;
    public List<HidingSpot> HidingSpots { private set; get; }
    [HideInInspector] public BoxCollider Col { get; private set; }
    [SerializeField] private GameObject Place;
    [SerializeField] private int numOfXParts; // The number of places in the X-axis
    [SerializeField] private int numOfZParts; // The number of places in the Z-axis
    private List<BoxCollider> places = new List<BoxCollider>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Col = GetComponent<BoxCollider>();
    }

    void Start()
    {
        
        float colXSize = Col.bounds.size.x;
        float colZSize = Col.bounds.size.z;

        float XDivide = colXSize / numOfXParts;
        float ZDivide = colZSize / numOfZParts;

        Vector3 pos = Col.bounds.min;
        pos.y = Col.bounds.center.y;
        pos.x -= XDivide/2;
        pos.z -= ZDivide/2;

        // Variables used to set the name of the places
        int x = 0;
        int y = 0;

        // Instantiating the places
        for (int i = 0; i < numOfXParts; i++)
        {
            pos.x += XDivide;
            for (int j = 0; j < numOfZParts; j++)
            {
                pos.z += ZDivide;
                GameObject place = Instantiate(Place, pos, Quaternion.identity);
                BoxCollider placeCol = place.GetComponent<BoxCollider>();
                placeCol.size = new Vector3(XDivide, Col.bounds.size.y, ZDivide);
                place.name = $"Place {x}, {y}";
                places.Add(placeCol);
                x++;
            }

            pos.z = Col.bounds.min.z - (ZDivide / 2);
            x = 0;
            y++;
        }

        CreateDecisionTree();
    }

    /// <summary>
    /// Creates a new DecisionTree and sets it to the PlaceCreator.Tree
    /// </summary>
    public void CreateDecisionTree()
    {
        HidingSpots = new List<HidingSpot>();
        Tree = new DecisionTree();
        foreach (BoxCollider place in places)
        {
            HidingSpot placeSpot = place.transform.GetComponent<HidingSpot>();
            placeSpot.EnableUI();
            placeSpot.ID = placeSpot.name.GetHashCode();
            DecisionNode node = DecisionNode.CreateChild(Tree.RootNode, placeSpot, TypeOfObject.PLACE);
            Collider[] hidingSpots = Physics.OverlapBox(place.bounds.center, place.bounds.size / 2, Quaternion.identity, HidingLayer);


            foreach (Collider spot in hidingSpots)
            {
                HidingSpot hSpot = spot.GetComponent<HidingSpot>();
                hSpot.ID = hSpot.transform.position.GetHashCode();
                DecisionNode.CreateChild(node, hSpot, TypeOfObject.PLACE);

                hSpot.EnableUI();
                HidingSpots.Add(hSpot);
                
            }
        }
    }

    /// <summary>
    /// Returns the next hidingspot from the current DecisionTree for the AI to check, based on the probability of the HidingSpots
    /// </summary>
    /// <returns> The next hidingspot to check from the current DecisionTree</returns>
    public DecisionNode GetNextHidingSpot()
    {
        DecisionNode place = Tree.RootNode.GetNodeOfHighestProbability();
        DecisionNode spot = place.GetRandomHidingSpot();
        return spot;
    }

    // Used for drawing the bounds and names of the places
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(BoxCollider col in places)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            //DrawString(col.name, col.bounds.center, Color.green);
        }
    }

    //static void DrawString(string text, Vector3 worldPos, Color? colour = null)
    //{
    //    UnityEditor.Handles.BeginGUI();
    //    if (colour.HasValue) GUI.color = colour.Value;
    //    var view = UnityEditor.SceneView.currentDrawingSceneView;
    //    Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
    //    Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
    //    GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
    //    UnityEditor.Handles.EndGUI();
    //}
    // Source for the above function: https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0
}
