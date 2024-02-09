using UnityEngine;
using UnityEngine.EventSystems; // Required for event handlers

public class HoverSound : MonoBehaviour, IPointerEnterHandler // Interface for pointer enter event
{
    public AudioSource audioSource; // Assign in the inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play(); // Play the sound when the pointer hovers over the button
        }
    }
}
