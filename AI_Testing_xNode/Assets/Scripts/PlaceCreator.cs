using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceCreator : MonoBehaviour
{
    private BoxCollider col;

    public LayerMask HidingLayer;

    [SerializeField] GameObject Place;
    [SerializeField] int numOfXParts;
    [SerializeField] int numOfZParts;

    public DecisionTree Tree { private set; get; }

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

        // Variabler för att sätta namnet på objektet
        int x = 0;
        int y = 0;

        for (int i = 0; i < numOfXParts; i++)
        {
            pos.x += XDivide;
            for (int j = 0; j < numOfZParts; j++)
            {
                pos.z += ZDivide;
                GameObject place = Instantiate(Place, pos, Quaternion.identity);
                BoxCollider placeCol = place.GetComponent<BoxCollider>();
                placeCol.size = new Vector3(XDivide, col.bounds.size.y, ZDivide);
                place.name = $"Place {x}, {y}";
                places.Add(placeCol);
                x++;

            }

            pos.z = col.bounds.min.z - (ZDivide / 2);
            x = 0;
            y++;
        }

        CreateDecisionTree();
    }

    private void CreateDecisionTree()
    {
        Tree = new DecisionTree();
        foreach (BoxCollider place in places)
        {
            
            DecisionNode node = DecisionNode.CreateChild(Tree.RootNode, null, TypeOfObject.PLACE);
            Collider[] hidingSpots = Physics.OverlapBox(place.bounds.center, place.bounds.size, Quaternion.identity, HidingLayer);
            print(hidingSpots.Length + " name: " + place.gameObject.name);

            foreach(Collider spot in hidingSpots)
            {
                DecisionNode newSpot = DecisionNode.CreateChild(node, spot.GetComponent<HidingSpot>(), TypeOfObject.PLACE);
                //node.Children.Add(newSpot);
                print("ADD CHILD");

            }
        }

        print(Tree.ToString());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach(BoxCollider col in places)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            DrawString(col.name, col.bounds.center, Color.green);

        }
    }

    static void DrawString(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();
        if (colour.HasValue) GUI.color = colour.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        UnityEditor.Handles.EndGUI();
    }
    // Källa: https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0



}
