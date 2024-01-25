using System.Collections;
using UnityEngine;

public class SceneFade : MonoBehaviour
{
    public float fadeDuration = 2f;
    public Color fadeColor; // Ensure 'Color' is capitalized as it's a type
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); // Corrected 'Renderer' spelling
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(1, 0)); // Fixed method call and coroutine spelling
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(0, 1)); // Fixed method call
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime; // Corrected 'Time.deltaTime'
            yield return null;
        }

        // Set to final color after loop is done
        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        rend.material.SetColor("_Color", finalColor);
    }
}
