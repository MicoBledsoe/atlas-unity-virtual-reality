using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.Events; 
//My directives above

// This serializable struct can be used to store gesture data.
[System.Serializable] // Make CustomGesture serializable for Unity Inspector
public struct CustomGesture
{
    public string gestureName; // Name of the gesture.
    public List<Vector3> fingerPositions; // Position data of the fingers for this gesture.
    public UnityEvent onGestureRecognized; // Event to invoke when the gesture is recognized.

    // Initialize the UnityEvent to ensure it's not null.
    public CustomGesture(string name) // Constructor to initialize a new gesture
    {
        gestureName = name; // Set the gesture name
        fingerPositions = new List<Vector3>(); // Initialize the list of finger positions
        onGestureRecognized = new UnityEvent(); // Initialize the UnityEvent
    }
}

public class GestureDetector : MonoBehaviour // Main class for detecting gestures
{
    public float recognitionThreshold = 0.1f; // Threshold for gesture recognition
    public OVRSkeleton handSkeleton; // Reference to the hand skeleton
    public List<CustomGesture> definedGestures = new List<CustomGesture>(); // List of defined gestures
    public bool enableDebugMode = true; // Flag to enable debug mode
    private List<OVRBone> handBones; // List of bones in the hand
    private CustomGesture lastRecognizedGesture; // Store the last recognized gesture

    void Start() // Start is called before the first frame update
    {
        if (handSkeleton != null) // Check if handSkeleton is assigned
        {
            handBones = new List<OVRBone>(handSkeleton.Bones); // Initialize handBones with the skeleton's bones
        }
        else // If handSkeleton is not assigned
        {
            Debug.LogError("GestureDetector: handSkeleton is not assigned."); // Log an error message
        }

        // Initialize with a default gesture to avoid null reference
        lastRecognizedGesture = new CustomGesture("None"); // Set a default gesture
    }

    void Update() // Update is called once per frame
    {
        if (handSkeleton == null || handBones == null || handBones.Count == 0) // Check if necessary components are missing
        {
            return; // Early exit if we don't have what we need
        }

        if (enableDebugMode && Input.GetKeyDown(KeyCode.Space)) // Check if debug mode is enabled and space key is pressed
        {
            SaveCurrentGesture(); // Save the current gesture
        }

        CustomGesture currentGesture = RecognizeGesture(); // Recognize the current gesture
        bool isGestureRecognized = !currentGesture.Equals(new CustomGesture("None")); // Check if a gesture is recognized

        if (isGestureRecognized && !currentGesture.gestureName.Equals(lastRecognizedGesture.gestureName)) // Check if a new gesture is found
        {
            Debug.Log("New Gesture Found: " + currentGesture.gestureName); // Log the new gesture
            lastRecognizedGesture = currentGesture; // Update the last recognized gesture

            if (currentGesture.onGestureRecognized != null) // Check if the gesture has an associated event
            {
                currentGesture.onGestureRecognized.Invoke(); // Invoke the gesture's event
            }
            else // If the gesture does not have an associated event
            {
                Debug.LogWarning("GestureDetector: onGestureRecognized event is null for gesture: " + currentGesture.gestureName); // Log a warning message
            }
        }
    }

    void SaveCurrentGesture() // Method to save the current gesture
    {
        if (handBones == null) // Check if handBones is not initialized
        {
            return; // Early exit if handBones is not initialized
        }

        CustomGesture newGesture = new CustomGesture("New Gesture"); // Create a new gesture
        foreach (var bone in handBones) // Iterate through each bone in handBones
        {
            newGesture.fingerPositions.Add(handSkeleton.transform.InverseTransformPoint(bone.Transform.position)); // Add the bone's position to the new gesture
        }
        definedGestures.Add(newGesture); // Add the new gesture to the list of defined gestures
    }

    CustomGesture RecognizeGesture() // Method to recognize a gesture
    {
        CustomGesture recognizedGesture = new CustomGesture("None"); // Initialize recognizedGesture with a default value
        float currentMinDistance = Mathf.Infinity; // Initialize currentMinDistance with infinity

        foreach (var gesture in definedGestures) // Iterate through each defined gesture
        {
            float sumDistances = 0; // Initialize sumDistances
            bool gestureMismatch = false; // Flag for gesture mismatch

            for (int i = 0; i < Mathf.Min(handBones.Count, gesture.fingerPositions.Count); i++) // Iterate through each bone
            {
                Vector3 currentBonePosition = handSkeleton.transform.InverseTransformPoint(handBones[i].Transform.position); // Get the current bone position
                float distanceToGesture = Vector3.Distance(currentBonePosition, gesture.fingerPositions[i]); // Calculate distance to the gesture

                if (distanceToGesture > recognitionThreshold) // Check if the distance exceeds the threshold
                {
                    gestureMismatch = true; // Set gestureMismatch to true
                    break; // Exit the loop
                }

                sumDistances += distanceToGesture; // Add the distance to sumDistances
            }

            if (!gestureMismatch && sumDistances < currentMinDistance) // Check if the gesture matches and has the smallest distance
            {
                currentMinDistance = sumDistances; // Update currentMinDistance
                recognizedGesture = gesture; // Update recognizedGesture
            }
        }

        return recognizedGesture; // Return the recognized gesture
    }
}
