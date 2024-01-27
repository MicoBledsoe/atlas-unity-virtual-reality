using UnityEngine;
// my directives above

// Class to define the mapping between a VR target and an IK target
[System.Serializable]
public class VRToIKMapping
{
    public Transform vrReferencePoint; // VR target point
    public Transform ikEffector; // Corresponding IK target
    public Vector3 posOffset; // Positional offset for the mapping
    public Vector3 rotOffset; // Rotational offset for the mapping

    // Method to apply the mapping
    public void ApplyMapping()
    {
        ikEffector.position = vrReferencePoint.TransformPoint(posOffset); // Map position with offset
        ikEffector.rotation = vrReferencePoint.rotation * Quaternion.Euler(rotOffset); // Map rotation with offset
    }
}

public class IKFollowVR : MonoBehaviour
{
    [Range(0,1)]
    public float smoothness = 0.1f; // Smoothness for the rotation interpolation
    public VRToIKMapping headMapping; // Mapping for the head
    public VRToIKMapping leftHandMapping; // Mapping for the left hand
    public VRToIKMapping rightHandMapping; // Mapping for the right hand

    public Vector3 headToBodyOffset; // Offset between the head IK and the body position
    public float bodyYawOffset; // Additional Yaw offset for the body

    // Update method called every frame
    void LateUpdate()
    {
        // Set the position of the body based on the head's IK position plus an offset
        transform.position = headMapping.ikEffector.position + headToBodyOffset;
        
        // Calculate the yaw rotation based on the VR target's rotation
        float targetYaw = headMapping.vrReferencePoint.eulerAngles.y + bodyYawOffset;
        // Interpolate the rotation for smooth turning
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetYaw, 0), smoothness);

        // Apply the mappings to the head and hands
        headMapping.ApplyMapping();
        leftHandMapping.ApplyMapping();
        rightHandMapping.ApplyMapping();
    }
}
