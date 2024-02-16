using UnityEngine;
using UnityEngine.UI;//My directives above

public class UIWindowSizeControl : MonoBehaviour
{
    public CanvasScaler canvasScaler; //main UI CanvasScaler
    public Slider windowSizeSlider; //the slider that controls the window size

    void Start()
    {
        Debug.Log($"Initial UI Scale Factor: {canvasScaler.scaleFactor}"); // Debug initial scale factor
        windowSizeSlider.value = canvasScaler.scaleFactor; // Set slider's initial val

        // Adding listener for when slider val changes
        windowSizeSlider.onValueChanged.AddListener(AdjustWindowSize);
    }

    void AdjustWindowSize(float newScaleFactor)
    {
        Debug.Log($"Adjusting UI Window Size to Scale Factor: {newScaleFactor}"); // Debug new scale factor
        canvasScaler.scaleFactor = newScaleFactor; // Apply new scale factor
    }

    void OnDestroy()
    {
        // Removee listener to clean up
        windowSizeSlider.onValueChanged.RemoveListener(AdjustWindowSize);
    }
}
