//Marcus Lundqvist
//Niclas Älmeby

using UnityEngine;
using UnityEngine.UI;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private int playerProbability = 1;
    public int PlayerProbability { get { return playerProbability; } 
                                   private set { playerProbability = value; } }
    [SerializeField] private int probability = 1;
    public int Probability  { get { return probability; }
                              private set { probability = value; } }
    public bool visited = false;
    [SerializeField] private TypeOfObject type;
    public TypeOfObject Type { get { return type; }
                               private set { type = value; } }
    public int ID;
    private bool hasBeenUpdated = false;

    [SerializeField] private Canvas textCanvas;
    private Text textBox;

    public void Start()
    {
        textCanvas = GetComponentInChildren<Canvas>();
        if(textCanvas != null)
        {
            textCanvas.renderMode = RenderMode.WorldSpace;
            textCanvas.transform.position = transform.position + Vector3.up;
            textCanvas.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            textCanvas.worldCamera = Camera.main;
            textBox = textCanvas.GetComponentInChildren<Text>();
        }
		if(type == TypeOfObject.PLACE)
        {
            textCanvas.transform.rotation = Quaternion.Euler(25f, 0f, 0f);
            textBox.color = Color.green;
        }

        EventController.current.LowerProbabilityEvent += LowerProbability;
        EventController.current.SaveDataEvent += SaveData;
        EventController.current.ResetHidingEvent += ResetToStartingValues;
        
        LoadData();
    }

    /// <summary>
    /// Uppdates the HidingSpot's probability based on the distance the player was from the HidingSpot,
    /// The closer the player was to the HidingSpot the more probability increases
    /// </summary>
    /// <param name="playerPostion"> The position of the player </param>
    public void UpdateProbability(Vector3 playerPostion)
    {
        if (hasBeenUpdated == false)
        {
            hasBeenUpdated = true;
            int value = (int)((playerPostion - this.transform.position).magnitude);

            switch (value)
            {
                case 0:
                    Probability = Mathf.Clamp(Probability + 3, 1, 10);
                    break;
                case 1:
                    Probability = Mathf.Clamp(Probability + 2, 1, 10);
                    break;
                default:
                    Probability = Mathf.Clamp(Probability + 1, 1, 10);
                    break;
            }
            UpdateTextUI();
        }
    }

    /// <summary>
    /// Uppdates the HidingSpot's probability.
    /// </summary>
    /// <param name="addedProbability"> The amount to add to the probability</param>
    public void UpdateProbability(int addedProbability)
    {
        if (hasBeenUpdated == false)
        {
            hasBeenUpdated = true;
            Probability += addedProbability;
        }
        UpdateTextUI();
    }

    /// <summary>
    /// Lowers the HidingSpot's probability with -1.
    /// This function is used to "erode" the probability of all nodes that was not increased. 
    /// </summary>
    private void LowerProbability()
    {
        if (hasBeenUpdated == false)
        {
            Probability = Mathf.Clamp(Probability - 1, 1, 10);
        }
        UpdateTextUI();
    }

    /// <summary>
    /// Resets hasBeenYpdatedm to allow the HidingSpot to update its probability again;
    /// </summary>
    private void ResetToStartingValues()
    {
        hasBeenUpdated = false;
    }

    /// <summary>
    /// Saves the probability of the HidingSpot using the PlayerPrefs.
    /// </summary>
    private void SaveData()
    {
        PlayerPrefs.SetInt("HidingSpot" + ID, Probability);
    }

    /// <summary>
    /// Loads the probability of the HidingSpot using the PlayerPrefs.
    /// </summary>
    private void LoadData()
    {
        int temp = PlayerPrefs.GetInt("HidingSpot" + ID);

        if(temp != 0)
        {
            Probability = temp;
        }
        UpdateTextUI();
    }

    /// <summary>
    /// Updates the UI in the scene that shows the current probabilities
    /// </summary>
    private void UpdateTextUI()
    {
        if (textCanvas != null)
            if (type != TypeOfObject.PLACE)
                textBox.text = $"{Probability} : {PlayerProbability}";
            else
                textBox.text = $"{Probability}";
    }

    // Drawing the probability of the AI to choose the HidingSpot and the players probability to choose the same HidingSpot 
    //private void OnDrawGizmos()
    //{
    //    if(Type != TypeOfObject.PLACE)
    //        DrawString($"{Probability} : {PlayerProbability}", transform.position + (Vector3.up * 3), Color.blue); // Drawing the current probability of the HidingSpot and the player probability of choosing the HidingSpot 
    //    else
    //        DrawString($"{Probability}", transform.position + Vector3.up, Color.blue); // Drawing the current probability of the place
    //}

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
