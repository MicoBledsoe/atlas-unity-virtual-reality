using UnityEngine;
//my directives above ^.
public class DynamicMagnification : MonoBehaviour
{
    public Camera mainCamera; // Public var to assign the main CAM in the Inspector.
    public Camera magnifyingCamera; // Public va to assign a secondary camera used for magnification.
    public float magnificationStrength = 2.0f; // The factor by which the field of view is reduced to achieve magnification.

    // Update is called once per frame by Unity.
    void Update()
    {
        // Set the rotation of the magnifying camera to match that of the main camera for consistent orientation.
        magnifyingCamera.transform.rotation = mainCamera.transform.rotation;

        // Adjust the field of view of the magnifying camera to be the main camera's field of view divided by the magnification strength.
        // This effectively zooms in the magnifying camera's view.
        magnifyingCamera.fieldOfView = mainCamera.fieldOfView / magnificationStrength;
    }
}
