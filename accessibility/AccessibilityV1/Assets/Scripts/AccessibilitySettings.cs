using UnityEngine;
using UnityEngine.UI;
using TMPro;
//My directives above ^

public class AccessibilitySettings : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider windowSizeSlider;
    public Slider textSizeSlider;

    [Header("UI Elements")]
    public CanvasScaler canvasScaler; //main canvas scaler
    public TextMeshProUGUI[] textElements; //all text elements to be scaled

    void Start()
    {
        //listeners to the sliders to call the respective methods when their values change
        windowSizeSlider.onValueChanged.AddListener(HandleWindowSizeChange);
        textSizeSlider.onValueChanged.AddListener(HandleTextSizeChange);
    }

    private void HandleWindowSizeChange(float value)
    {
        // Adjust the scale factor of the canvas scaler.
        canvasScaler.scaleFactor = value;
    }

    private void HandleTextSizeChange(float value)
    {
        // Adjusts the font size of all text elements. 
        foreach (var textElement in textElements)
        {
            textElement.fontSize = Mathf.Lerp(10f, 30f, value);
        }
    }

    void OnDestroy()
    {
        //remove listeners when the script or GameObject is destroyed
        windowSizeSlider.onValueChanged.RemoveListener(HandleWindowSizeChange);
        textSizeSlider.onValueChanged.RemoveListener(HandleTextSizeChange);
    }
}
