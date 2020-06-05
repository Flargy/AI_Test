//Niclas Älmeby

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleChanger : MonoBehaviour
{
    [SerializeField] private Text timeScaleText;
    [SerializeField] private Slider TimeScaleSlider;

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
        timeScaleText.text = "Time Scale = 1";
    }

    public void SlideTimeScale()
    {
        Time.timeScale = TimeScaleSlider.value;
        timeScaleText.text = $"Time Scale = {TimeScaleSlider.value}";
    }
}
