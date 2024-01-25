using UnityEngine;
using System.Collections;

public class VideoControlCanvas : MonoBehaviour
{
    public GameObject CanvasGameObject; // Assign your canvas here
    private float inactivityTimer = 0f;
    private bool isCanvasActive = true;
    private float inactivityThreshold = 5f; // 5 seconds of inactivity

    void Update()
    {
        // Check for trigger button press to show canvas
        if (Input.GetButtonDown("TriggerButton")) // Replace "TriggerButton" with your actual button name
        {
            CanvasGameObject.SetActive(true);
            isCanvasActive = true;
            inactivityTimer = 0f;
        }

        // Update inactivity timer and hide canvas if inactive for too long
        if (isCanvasActive)
        {
            inactivityTimer += Time.deltaTime;
            if (inactivityTimer >= inactivityThreshold)
            {
                CanvasGameObject.SetActive(false);
                isCanvasActive = false;
            }
        }
    }

    // Call this method when any interaction button on the canvas is used
    public void OnCanvasInteraction()
    {
        inactivityTimer = 0f; // Reset timer on interaction
    }
}
