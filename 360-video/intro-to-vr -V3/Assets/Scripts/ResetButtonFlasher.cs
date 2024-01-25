using UnityEngine;
//^My Directives

public class ResetButtonFlasher : MonoBehaviour //My blueprint
{
    public float RESETPulser = 1.5f; //Rate at which the Betty Text pulses
    public float maxScale = 1.1f; //MAX scale of the obj during BettyPulse
    public float minScale = 1.0f; //MIN scale of the obj during BettyPulse
    private RectTransform rectTransform; //reference to the obj RectTransform

    void Awake() //my script instnace is being loaded
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update() //updating every frameee
    {
        // the scale of the object between minScale and maxScale at a rate of BettyPulser !
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * RESETPulser, 1));
        rectTransform.localScale = new Vector3(scale, scale, 1f); //applying the caculated scale to the obj
    }
}