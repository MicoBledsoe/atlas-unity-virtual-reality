using UnityEngine; 
using UnityEngine.Events; 
using UnityEngine.EventSystems; 
using System.Collections.Generic; 
//My directives above ^.

// Define the Highlighter class that inherits from MonoBehaviour and implements pointer event handlers.
public class Highlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onPointerEnter; // Event triggered when the pointer enters the GameObject.
    public UnityEvent onPointerExit; // Event triggered when the pointer exits the GameObject.

    public DynamicMagnification dynamicMagnification; // Reference to the DynamicMagnification script for toggling magnification.

    public float gazeHoldTime = 2.0f; // Duration in seconds that gaze must be held to trigger an action.
    private float gazeTimer = 0.0f; // Internal timer to track gaze duration.
    private bool isGazing = false; // Flag to track if the current state is gazing.

    public GameObject canvasToDisable; // The canvas that will be toggled on or off.
    public Camera mainCamera; // The main camera in the scene, used for positioning.

    public GameObject objectToToggle; // The GameObject that will be toggled by the script.

    void Awake() // Called when the script instance is being loaded.
    {
        if (canvasToDisable != null) // Check if a canvas is assigned.
        {
            canvasToDisable.SetActive(false); // Deactivate the canvas on start. ( CURRENTLY NEEDS BUG FIX: I HAVE TO CHANGE TRUE OR FALSE CURRENTLY )
        }
        else
        {
            Debug.LogWarning("Reference to the canvas is not set in the Inspector.");
        }
    }

    void Update() // Called once per frame.
    {
        if (isGazing) // If the user is currently gazing at the object.
        {
            gazeTimer += Time.deltaTime; // Increment the gaze timer.
            if (gazeTimer >= gazeHoldTime) // Check if the gaze has been held long enough.
            {
                ResetGazeDetection(); // Reset gaze detection and potentially trigger actions here.
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // Called when the pointer enters the object's area.
    {
        isGazing = true; // Set the gazing flag to true.
        onPointerEnter.Invoke(); // Invoke any actions assigned to the onPointerEnter event.
    }

    public void OnPointerExit(PointerEventData eventData) // Called when the pointer exits the object's area.
    {
        ResetGazeDetection(); // Reset gaze detection.
        onPointerExit.Invoke(); // Invoke any actions assigned to the onPointerExit event.
    }

    private void ResetGazeDetection() // Resets the gaze detection mechanism.
    {
        isGazing = false; // Reset the gazing flag to false.
        gazeTimer = 0.0f; // Reset the gaze timer to 0.
    }

    public void ToggleMagnificationDynamic() // Toggles magnification on or off dynamically.
    {
        if (objectToToggle != null) // Checking to see if there's an object assigned to toggle.
        {
            bool isActive = !objectToToggle.activeSelf; // Determine the new active state.
            objectToToggle.SetActive(isActive); // Set the object's active state.

            if (dynamicMagnification != null) // Check if dynamic magnification is assigned.
            {
                dynamicMagnification.enabled = isActive; // Enable or disable dynamic magnification.
            }

            if (isActive && mainCamera != null) // If activating and a main camera is assigned.
            {
                // Position the object in front of the camera.
                Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
                objectToToggle.transform.position = newPosition;
                
                // Orient the object to face the camera.
                objectToToggle.transform.rotation = Quaternion.LookRotation(objectToToggle.transform.position - mainCamera.transform.position);
            }
        }
    }
}
