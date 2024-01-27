using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//My Directives above

public class IKFootSolver : MonoBehaviour
{
    public bool isMovingForward; // Flag to determine if the foot is moving forward

    [SerializeField] LayerMask terrainLayer = default; // Layer of the terrain to interact with
    [SerializeField] Transform body = default; // Reference to the body to which the foot is attached
    [SerializeField] IKFootSolver otherFoot = default; // Reference to the other foot for coordination
    [SerializeField] float speed = 4; // Speed of the foot movement
    [SerializeField] float stepDistance = .2f; // Minimum distance to trigger a new step
    [SerializeField] float stepLength = .2f; // Length of each step
    [SerializeField] float sideStepLength = .1f; // Length of sidestep

    [SerializeField] float stepHeight = .3f; // Height of the foot during a step
    [SerializeField] Vector3 footOffset = default; // Offset of the foot position

    public Vector3 footRotOffset; // Offset for the rotation of the foot
    public float footYPosOffset = 0.1f; // Vertical offset of the foot

    public float rayStartYOffset = 0; // Vertical offset for starting the raycast
    public float rayLength = 1.5f; // Length of the raycast
    
    float footSpacing; // Horizontal spacing of the foot from the body
    Vector3 oldPosition, currentPosition, newPosition; // Positions for foot movement
    Vector3 oldNormal, currentNormal, newNormal; // Normals for foot orientation
    float lerp; // Interpolation value for smooth movement

    private void Start()
    {
        footSpacing = transform.localPosition.x; // Initialize foot spacing
        currentPosition = newPosition = oldPosition = transform.position; // Set initial positions
        currentNormal = newNormal = oldNormal = transform.up; // Set initial normals
        lerp = 1; // Initialize lerp value
    }

    void Update()
    {
        transform.position = currentPosition + Vector3.up * footYPosOffset; // Update the foot position
        transform.localRotation = Quaternion.Euler(footRotOffset); // Apply rotation offset

        // Create a raycast from the body in the direction of the foot
        Ray ray = new Ray(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);

        // Draw ray in the editor for debugging
        Debug.DrawRay(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);
            
        if (Physics.Raycast(ray, out RaycastHit info, rayLength, terrainLayer.value)) // Perform the raycast
        {
            // Check if the foot needs to move
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0; // Reset lerp for new movement
                // Calculate the direction for the new step
                Vector3 direction = Vector3.ProjectOnPlane(info.point - currentPosition,Vector3.up).normalized;

                // Calculate the angle between the body forward direction and step direction
                float angle = Vector3.Angle(body.forward, body.InverseTransformDirection(direction));

                // Determine if the step is forward or sideways
                isMovingForward = angle < 50 || angle > 130;

                // Set the new position and normal based on the step type
                if(isMovingForward)
                {
                    newPosition = info.point + direction * stepLength + footOffset;
                    newNormal = info.normal;
                }
                else
                {
                    newPosition = info.point + direction * sideStepLength + footOffset;
                    newNormal = info.normal;
                }
            }
        }

        // Animate the foot movement
        if (lerp < 1)
        {
            // Calculate the interpolated position
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            // Add height for the step
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition; // Update the current position
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp); // Update the current normal
            lerp += Time.deltaTime * speed; // Increment lerp based on speed
        }
        else
        {
            oldPosition = newPosition; // Update the old position for the next cycle
            oldNormal = newNormal; // Update the old normal for the next cycle
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set Gizmocolor for the new position
        Gizmos.DrawSphere(newPosition, 0.1f); // Draw a sphere at the new position
    }

    // Method to check if the foot is currently moving
    public bool IsMoving()
    {
        return lerp < 1; // Returns true if the foot is in the process of moving
    }
}
