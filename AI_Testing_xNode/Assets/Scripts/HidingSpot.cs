// Marcus Lundqvist
// Niclas Älmeby

using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
    /// /// <param name="maxRange"> The max distance that will give a updated probability </param>
    public void UpdateProbability(Vector3 playerPostion, float maxRange)
    {
        if (hasBeenUpdated == false)
        {
            hasBeenUpdated = true;
            int value = (int)((playerPostion - this.transform.position).magnitude);
            if(value == 0)
            {
                Probability = Mathf.Clamp(3, 1, 10);
            }
            else
            {
                Debug.Log((maxRange / value) + " " + Mathf.Max(3, (maxRange / value)));
                int newValue = Mathf.Max(1, (int)Math.Round(Probability + (maxRange - value)));
                Probability = Mathf.Clamp(newValue, 1, 10); // note the probability given will increese if a higher max radius on the SphereCast is choosen in AISensor.
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
    /// Updates the UI in the scene that shows the current probabilities.
    /// </summary>
    private void UpdateTextUI()
    {
        if (textCanvas != null)
            if (type != TypeOfObject.PLACE)
                textBox.text = $"{Probability} : {PlayerProbability}";
            else
                textBox.text = $"{Probability}";
    }

    /// <summary>
    /// Disables the UI of the hiding spot
    /// </summary>
    public void DisableUI()
    {
        if (textCanvas != null)
            textCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enables the UI of the hiding spot
    /// </summary>
    public void EnableUI()
    {
        if(textCanvas != null)
            textCanvas.gameObject.SetActive(true);
    }
}
