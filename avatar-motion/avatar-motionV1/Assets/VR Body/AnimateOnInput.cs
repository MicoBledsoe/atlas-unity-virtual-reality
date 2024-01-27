using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//my directives above

// Define a serializable class to associate animation parameters with input actions
[System.Serializable]
public class AnimInputMapping
{
    public string animParamName; // Name of the parameter in Animator
    public InputActionProperty inputAction; // Input action that triggers the animation
}

public class InputBasedAnimator : MonoBehaviour
{
    public List<AnimInputMapping> animInputMappings; // List to hold all our animation input mappings
    public Animator characterAnimator; // Reference to the Animator component

    // Update is called once per frame
    void Update()
    {
        foreach (var mapping in animInputMappings) // Loop through each animation input mapping
        {
            float inputValue = mapping.inputAction.action.ReadValue<float>(); // Read the current value of the input action
            characterAnimator.SetFloat(mapping.animParamName, inputValue); // Apply that value to the corresponding Animator parameter
        }
    }
}
