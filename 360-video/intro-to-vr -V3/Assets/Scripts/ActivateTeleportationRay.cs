using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
//my directives above^

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel; // Corrected typo "rgihtCancel" to "rightCancel"

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    void Update()
    {
        // Left teleportation ray
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftHitIndex, out bool leftIsValidTarget);
        bool leftActivateValue = leftActivate.action.ReadValue<float>() > 0;
        bool leftCancelValue = leftCancel.action.ReadValue<float>() > 0;
        leftTeleportation.SetActive(!isLeftRayHovering && !leftCancelValue && leftActivateValue);

        // Right teleportation ray
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightHitIndex, out bool rightIsValidTarget);
        bool rightActivateValue = rightActivate.action.ReadValue<float>() > 0;
        bool rightCancelValue = rightCancel.action.ReadValue<float>() > 0;
        rightTeleportation.SetActive(!isRightRayHovering && !rightCancelValue && rightActivateValue);
    }
}
