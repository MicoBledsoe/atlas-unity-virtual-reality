using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad = "MainScene"; // The name of the scene you want to load.
    private bool isGazing = false;
    private float gazeDuration = 0f;
    public float requiredGazeTime = 2f; // Time in seconds the user needs to gaze at the button to trigger the scene switch.

    void Update()
    {
        if (isGazing)
        {
            gazeDuration += Time.deltaTime;
            if (gazeDuration >= requiredGazeTime)
            {
                LoadScene();
                gazeDuration = 0f; // Reset gaze duration in case we come back to this menu.
            }
        }
    }

    public void OnGazeEnter()
    {
        Debug.Log("Gaze Started");
        isGazing = true;
        gazeDuration = 0f; // Reset gaze duration every time gaze enters.
    }

    public void OnGazeExit()
    {
        isGazing = false;
        gazeDuration = 0f; // Reset gaze duration to ensure it only counts continuous gaze time.
    }

    private void LoadScene()
    {
        Debug.Log($"Attempting to load scene: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}
