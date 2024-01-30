using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//My DIrectives above

// Serializable class to map VR and IK targets with offsets
[System.Serializable]
public class MapTransforms
{
    public Transform VRTarget; // The VR target transform
    public Transform IKTarget; // The IK target transform for mapping
    public Vector3 TrackingPositionOffset; // Offset for position tracking
    public Vector3 TrackingRotationOffset; // Offset for rotation tracking

    // Method to map VR target position and rotation to IK target
    public void VRMapping()
    {
        // Map position with offset
        IKTarget.position = VRTarget.TransformPoint(TrackingPositionOffset);
        // Map rotation with offset
        IKTarget.rotation = VRTarget.rotation * Quaternion.Euler(TrackingRotationOffset);
    }
}

// Main controller for avatar movement and animation
public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransforms head; // MapTransforms instance for head
    [SerializeField] private MapTransforms leftHand; // MapTransforms for left hand
    [SerializeField] private MapTransforms rightHand; // MapTransforms for right hand

    [SerializeField] private float turnSmoothness; // Smoothness of the turn
    [SerializeField] private Transform IKHead; // IK head transform
    [SerializeField] private Vector3 headBodyOffset; // Offset between head and body

    // Update avatar's position and orientation every frame
    private void LateUpdate()
    {
        // Update position based on IK head position and offset
        transform.position = IKHead.position + headBodyOffset;
        // Smoothly update the forward direction of the avatar
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        // Apply VR mapping to the head and hands
        head.VRMapping();
        leftHand.VRMapping();
        rightHand.VRMapping();
    }
}
