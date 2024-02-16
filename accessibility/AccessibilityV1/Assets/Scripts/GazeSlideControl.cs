using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
//My directives above

public class GazeSliderControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider slider;
    private bool isGazing = false;
    private float gazeTimer = 0.0f;
    public float gazeDurationToActivate = 2.0f; // Time in seconds to gaze before activating slider adjustment

    void Update()
    {
        // Check if the user is gazing at the slider and start the timer
        if (isGazing)
        {
            gazeTimer += Time.deltaTime;
            Debug.Log($"Gazing... Timer: {gazeTimer}"); // Debug statement to verify timer is working
            if (gazeTimer >= gazeDurationToActivate)
            {
                AdjustSliderValue(); // Adjust the slider value when the gaze duration is met
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Start tracking gaze when the pointer enters the slider
        isGazing = true;
        gazeTimer = 0.0f; // Reset gaze timer
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop tracking gaze when the pointer exits the slider
        ResetGazeDetection();
    }

    private void AdjustSliderValue()
    {
        Debug.Log("Adjusting Slider Value"); // tooConfirm this method is called
        slider.value += 0.01f; // Increment slider value. Customize this logic as needed.
    }

    private void ResetGazeDetection()
    {
        isGazing = false;
        gazeTimer = 0.0f;
    }
}
