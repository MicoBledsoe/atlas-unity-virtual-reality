using UnityEngine;
using UnityEngine.EventSystems;

public class GazeDetector : MonoBehaviour
{
    public float gazeTime = 2f; // Time in seconds to activate the button
    private float gazeTimer = 0f;
    private GameObject currentGazeTarget;
    private AudioSource audioSource; // AudioSource for playing sound on gaze

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ensure there's an AudioSource component attached to the same GameObject
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit a UI button
            if (hit.collider.gameObject.CompareTag("UIButton")) // Make sure your UI buttons have the "UIButton" tag
            {
                if (currentGazeTarget == hit.collider.gameObject)
                {
                    // Increase the gaze timer if we're still looking at the same button
                    gazeTimer += Time.deltaTime;

                    // If we've gazed long enough, trigger the button's action
                    if (gazeTimer >= gazeTime)
                    {
                        // Play sound
                        if (audioSource && !audioSource.isPlaying)
                        {
                            audioSource.Play();
                        }
                        gazeTimer = 0f; // Reset the gaze timer
                    }
                }
                else
                {
                    // We've gazed onto a new button
                    currentGazeTarget = hit.collider.gameObject;
                    gazeTimer = 0f; // Reset the gaze timer
                }
            }
        }
        else
        {
            // If we're not gazing at anything, reset the gaze target and timer
            currentGazeTarget = null;
            gazeTimer = 0f;
        }
    }
}
