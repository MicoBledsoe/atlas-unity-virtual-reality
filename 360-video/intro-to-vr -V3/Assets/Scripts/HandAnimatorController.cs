using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//^My Directives Above

public class HandAnimatorController : MonoBehaviour
{
    // attribute makes private fields editable within the Inspector
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator anim; // Reference to the Animator component

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Gets the Animator component attached to this GameObject
        anim = GetComponent<Animator>();
    }

    // Updating is called once per frame
    private void Update()
    {
        // Reads the current value of the trigger and grip actions
        float triggerVal = triggerAction.action.ReadValue<float>();
        float gripVal = gripAction.action.ReadValue<float>();

        // Sets the corresponding values in the Animator
        anim.SetFloat("Trigger", triggerVal);
        anim.SetFloat("Grip", gripVal);
    }
}
