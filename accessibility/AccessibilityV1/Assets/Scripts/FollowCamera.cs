using UnityEngine;
//my directives above ^.

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0f, 0f, 2f); // Distance in front of the camera.
    public bool maintainFixedOrientation = true; // Set to true to keep the canvas facing the player.

    private Quaternion initialRotation;

    void Start()
    {
        if (maintainFixedOrientation)
        {
            // Save the initial rotation to maintain it.
            initialRotation = transform.rotation;
        }
    }

    void LateUpdate()
    {
        // Update position to follow the camera with an offset.
        transform.position = cameraTransform.position + cameraTransform.forward * offset.z;

        if (maintainFixedOrientation)
        {
            // Keep the initial rotation regardless of how the camera moves.
            transform.rotation = initialRotation;
        }
        else
        {
            // Optionally, could make the canvas always face the camera.
            transform.LookAt(cameraTransform);
        }
    }
}
